using DotnetSkeleton.EmailModule.Domain.Entities;
using DotnetSkeleton.EmailModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.EmailModule.Domain.Models.Requests;
using DotnetSkeleton.EmailModule.EmailWorker.Services.Interfaces;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Text;
using static DotnetSkeleton.SharedKernel.Utils.Constant;

namespace DotnetSkeleton.EmailModule.EmailWorker.Services;

public class EmailQueueService : IEmailQueueService
{
    #region Private members
    private readonly ILogger<EmailQueueService> _logger;
    private readonly MailOptions _mailOptions;

    private readonly ICommunicationTemplateRepository _communicationTemplateRepository;
    private readonly ICommunicationRepository _communicationRepository;
    private readonly IEmailMetadataRepository _emailMetadataRepository;
    private readonly IUserRepository _userRepository;
    #endregion

    #region Constructor
    public EmailQueueService(IOptionsMonitor<MailOptions> mailOptions,
        ICommunicationTemplateRepository communicationTemplateRepository,
        ICommunicationRepository communicationRepository, IEmailMetadataRepository emailMetadataRepository,
        IUserRepository userRepository, ILogger<EmailQueueService> logger)
    {
        _mailOptions = mailOptions.CurrentValue;
        _communicationTemplateRepository = communicationTemplateRepository;
        _communicationRepository = communicationRepository;
        _emailMetadataRepository = emailMetadataRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    #endregion

    #region Public methods

    /// <summary>
    /// Handles an outgoing email request by validating the request, saving communication data, sending the email, and updating the communication status.
    /// </summary>
    /// <param name="email">The outgoing email request containing the details of the email to be sent.</param>
    /// <returns> The task result contains a boolean value indicating whether the email was processed successfully. </returns>
    /// <remarks>
    /// - The method performs the following steps:
    ///   1. Validates the outgoing email request.
    ///   2. Saves the communication data related to the email.
    ///   3. Sends the email based on the provided request and template.
    ///   4. Updates the communication status if the email is successfully sent.
    /// - In case of failure at any stage, an appropriate error is logged and the process is aborted.
    /// - Exceptions during the process are caught and logged, and the method returns false.
    /// </remarks>
    public async Task<bool> OutgoingEmailHandlerAsync(OutgoingEmailRequest email)
    {
        try
        {
            // Step 1. Validate the request
            email.Sender = email.EmailType == Constant.EmailType.NoReply ? _mailOptions.NoReply.Mail : _mailOptions.MailSupport.Mail;
            var (validationResult, sender, template) = await ValidateOutgoingEmailAsync(email);
            if (!validationResult)
            {
                return false;
            }

            // Step 2. Save communication and related data
            var (saveOutgoingEmailResult, communication) = await SaveOutgoingEmailDataAsync(email, sender!, template);
            if (!saveOutgoingEmailResult)
            {
                _logger.LogError("[EmailQueueService] An error has been occurred when saving communication data");
                return false;
            }

            // Step 3. Send the email
            try
            {
                var sendEmailResult = await SendEmailAsync(email, template);
                if (!sendEmailResult)
                {
                    await UpdateCommunicationStatusAsync(communication, CommunicationStatus.Failed);
                    return false;
                }
            }
            catch (Exception)
            {
                await UpdateCommunicationStatusAsync(communication, CommunicationStatus.Failed);
                return false;
            }


            // Step 4. Update the email status
            await UpdateCommunicationStatusAsync(communication, CommunicationStatus.Delivered);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("[EmailQueueService] Exception occurred while processing send mail. {ex}", Helpers.BuildErrorMessage(ex));
            return false;
        }
    }

    #endregion

    #region Private methods
    /// <summary>
    /// Validates the outgoing email request by checking if the sender exists and if the specified communication template is available.
    /// Returns a response indicating the validation result along with the sender and template details if valid.
    /// </summary>
    /// <param name="sendEmailRequest">The request object containing the details of the outgoing email.</param>
    /// <returns>
    /// A tuple containing:
    /// <see cref="BaseResponse"/> indicating the validation result,
    /// <see cref="User"/> object representing the sender if found,
    /// <see cref="CommunicationTemplate"/> object if the template is found.
    /// </returns>
    private async Task<(bool, User?, CommunicationTemplate?)> ValidateOutgoingEmailAsync(OutgoingEmailRequest sendEmailRequest)
    {
        var sender = await _userRepository.FindOneAsync(_ => _.Email == sendEmailRequest.Sender);
        if (sender is null)
        {
            _logger.LogError("[EmailQueueService] Not found or invalid sender {sender}", sendEmailRequest.Sender);
            return (false, null, null);
        }

        if (!string.IsNullOrEmpty(sendEmailRequest.TemplateName))
        {
            var template = await _communicationTemplateRepository.FindOneAsync(_ => _.TemplateName == sendEmailRequest.TemplateName);
            if (template is not null)
            {
                return (true, sender, template);
            }

            _logger.LogError("[EmailQueueService] Not found or invalid email template {templateName}", sendEmailRequest.TemplateName);
            return (false, sender, null);
        }

        _logger.LogInformation("[EmailQueueService] The email is sending without template");
        return (true, sender, null);
    }

    /// <summary>
    /// Saves the communication details and associated email metadata for an outgoing email request.
    /// Creates and persists a new <see cref="Communication"/> record and an associated <see cref="EmailMetadata"/> record.
    /// Returns a tuple indicating the success of the email metadata saving operation and the result of adding the communication record.
    /// </summary>
    /// <param name="sendEmailRequest">The request object containing the details of the outgoing email.</param>
    /// <param name="sender">The <see cref="User"/> object representing the sender of the email.</param>
    /// <param name="template">The <see cref="CommunicationTemplate"/> object used for the email template.</param>
    /// <returns>
    /// A tuple containing:
    /// <see cref="bool"/> indicating the success of saving the email metadata, and the result of adding <see cref="Communication"/>
    /// </returns>
    private async Task<(bool, Communication)> SaveOutgoingEmailDataAsync(OutgoingEmailRequest sendEmailRequest, User sender, CommunicationTemplate? template)
    {
        var utcNow = DateTime.UtcNow;

        // Create and save Communication
        var communication = new Communication
        {
            CommunicationDatetime = utcNow,
            CommunicationType = CommunicationType.Email,
            Direction = CommunicationDirection.Outgoing,
            SenderId = sender.UserId,
            SenderInfo = sender.Email,
            Status = CommunicationStatus.Queued,
            ReceiverId = null,
            ReceiverInfo = string.Join(", ", sendEmailRequest.To),
        };

        var addCommunicationResult = await _communicationRepository.AddAsync(communication);

        // Create and save EmailMetadata
        // If a specific content is used instead of a template, it needs to be uploaded to a storage service (which hasn't been implemented yet)
        // And the file's URL from the storage service should be stored in the EmailMetadata table
        var emailMetadata = new EmailMetadata
        {
            CommunicationId = communication.CommunicationId,
            CommunicationTemplateId = template?.CommunicationTemplateId ?? null,
            Subject = string.IsNullOrEmpty(sendEmailRequest.TemplateName) ? sendEmailRequest.Subject : template?.Subject,
            ContentUrl = null,
            ErrorMessage = null
        };

        var addEmailMetadataResult = await _emailMetadataRepository.AddAsync(emailMetadata);

        return (addEmailMetadataResult is { CommunicationId: > 0, EmailMetadataId: > 0 }, addCommunicationResult);
    }

    /// <summary>
    /// Updates the status of the specified <see cref="Communication"/> record in the repository.
    /// </summary>
    /// <param name="communication">The <see cref="Communication"/> object whose status is to be updated.</param>
    /// <param name="status">The new status to be set for the communication record.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    private async Task UpdateCommunicationStatusAsync(Communication communication, string status)
    {
        communication.Status = status;
        await _communicationRepository.UpdateAsync(communication);
    }

    /// <summary>
    /// Builds the email content based on the provided email request.
    /// </summary>
    /// <param name="sendEmailRequest"> <see cref="OutgoingEmailRequest"/> The email request input</param>
    /// <param name="template"></param>
    /// <returns> A string that represent for the email's content </returns>
    private async Task<string> EmailContentBuildingAsync(OutgoingEmailRequest sendEmailRequest, CommunicationTemplate? template)
    {
        var templateContent = new StringBuilder();
        if (!string.IsNullOrEmpty(template?.TemplateName))
        {
            using var reader = new StreamReader(template.TemplateContentUrl);
            templateContent = new StringBuilder(await reader.ReadToEndAsync());
        }

        var emailContent = templateContent.ToString();
        if (string.IsNullOrEmpty(emailContent) || string.IsNullOrWhiteSpace(emailContent))
        {
            throw new Exception("[EmailQueueService] Email template can not be null or empty");
        }

        if (sendEmailRequest.TemplatePlaceHolders == null || !sendEmailRequest.TemplatePlaceHolders.Any())
        {
            return emailContent;
        }

        // Replace placeholders in the email template
        foreach (var (key, value) in sendEmailRequest.TemplatePlaceHolders)
        {
            emailContent = emailContent.Replace(key, value);
        }

        return emailContent;
    }

    /// <summary>
    /// Sends an email based on the provided request and template. It builds the email message, connects to the SMTP server, 
    /// authenticates, and sends the email asynchronously. Returns a boolean indicating the success or failure of the email sending process.
    /// </summary>
    /// <param name="emailRequest">The request object containing the details of the outgoing email.</param>
    /// <param name="template">The <see cref="CommunicationTemplate"/> used to build the email content if not provided in the request.</param>
    /// <returns> <see cref="Task{Boolean}"/> indicating whether the email was successfully sent or not.</returns>
    /// <exception cref="Exception">Throws if the email body is null or empty.</exception>
    private async Task<bool> SendEmailAsync(OutgoingEmailRequest emailRequest, CommunicationTemplate? template)
    {
        try
        {
            _logger.LogInformation("[EmailQueueService] Start process to send email");

            // Create a new email message
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(emailRequest.Sender)
            };

            email.Subject = string.IsNullOrEmpty(emailRequest.TemplateName) ? emailRequest.Subject : template?.Subject;
            email.To.AddRange(emailRequest.To.Select(MailboxAddress.Parse));
            if (emailRequest.Cc != null && emailRequest.Cc.Any())
            {
                email.Cc.AddRange(emailRequest.Cc.Select(MailboxAddress.Parse));
            }

            if (emailRequest.Bcc != null && emailRequest.Bcc.Any())
            {
                email.Bcc.AddRange(emailRequest.Bcc.Select(MailboxAddress.Parse));
            }

            // Create the email body content. If the email body is provided, priority to use it. Otherwise, build the email content
            var emailBody = !string.IsNullOrEmpty(emailRequest.Body) && !string.IsNullOrWhiteSpace(emailRequest.Body)
                ? emailRequest.Body
                : await EmailContentBuildingAsync(emailRequest, template);

            if (string.IsNullOrEmpty(emailBody) || string.IsNullOrWhiteSpace(emailBody))
            {
                throw new Exception("[EmailQueueService] Email body can not be null or empty");
            }

            var builder = new BodyBuilder { HtmlBody = emailBody };
            email.Body = builder.ToMessageBody();

            // Get credentials
            var (host, port, password) = emailRequest.EmailType == Constant.EmailType.NoReply
                ? (_mailOptions.NoReply.Host, _mailOptions.NoReply.Port, _mailOptions.NoReply.Password)
                : (_mailOptions.MailSupport.Host, _mailOptions.MailSupport.Port, _mailOptions.MailSupport.Password);

            // Create a new SMTP client
            using var client = new SmtpClient { CheckCertificateRevocation = false };

            // Connect to the SMTP server
            await client.ConnectAsync(host, port, SecureSocketOptions.StartTls).ConfigureAwait(false);
            _logger.LogInformation("[EmailQueueService] Smtp client connected");

            // Authenticate with the SMTP server
            await client.AuthenticateAsync(emailRequest.Sender, password).ConfigureAwait(false);

            // Send the email async
            await client.SendAsync(email).ConfigureAwait(false);

            // Disconnect from the SMTP server
            await client.DisconnectAsync(true).ConfigureAwait(false);
            _logger.LogInformation("[EmailQueueService] Sent email and disconnected successfully");

            return true;
        }
        catch (SmtpCommandException ex)
        {
            _logger.LogError("[EmailQueueService] SMTP command error: {message}, StatusCode: {statusCode}",
                ex.Message, ex.StatusCode);
            return false;
        }
        catch (SmtpProtocolException ex)
        {
            _logger.LogError("[EmailQueueService] SMTP protocol error: {message}", ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError("[EmailQueueService] {exception}", Helpers.BuildErrorMessage(ex));
            return false;
        }
    }

    #endregion
}