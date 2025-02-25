using DotnetSkeleton.EmailModule.Domain.Entities;
using DotnetSkeleton.EmailModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.EmailModule.Domain.Models.Requests;
using DotnetSkeleton.EmailModule.Domain.Resources;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.Utils.RabbitMqService.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using static DotnetSkeleton.SharedKernel.Utils.Constant;
using IUserRepository = DotnetSkeleton.EmailModule.Domain.Interfaces.Repositories.IUserRepository;
using User = DotnetSkeleton.EmailModule.Domain.Entities.User;

namespace DotnetSkeleton.EmailModule.Application.Services;

public class EmailService : DotnetSkeleton.EmailModule.Domain.Interfaces.Services.IEmailService
{
    #region Private Fields

    // Repositories
    private readonly IUserRepository _userRepository;
    private readonly ICommunicationRepository _communicationRepository;
    private readonly ICommunicationTemplateRepository _communicationTemplateRepository;
    private readonly IEmailMetadataRepository _emailMetadataRepository;

    // Services
    private readonly IRabbitMqService<OutgoingEmailRequest> _rabbitMqService;

    // Options
    private readonly MailOptions _mailOptions;
    private readonly RabbitMqOptions _rabbitMqOptions;

    // Others
    private readonly ILogger<EmailService> _logger;
    private readonly IStringLocalizer<Resources> _localizer;

    #endregion

    #region Constructor
    public EmailService(IOptionsMonitor<MailOptions> mailOptions, ILogger<EmailService> logger,
        IStringLocalizer<Resources> localizer, IEmailMetadataRepository emailMetadataRepository,
        ICommunicationTemplateRepository communicationTemplateRepository,
        ICommunicationRepository communicationRepository, IUserRepository userRepository,
        IOptionsMonitor<RabbitMqOptions> rabbitMqOptions, IRabbitMqService<OutgoingEmailRequest> rabbitMqService)
    {
        _mailOptions = mailOptions.CurrentValue;
        _rabbitMqOptions = rabbitMqOptions.CurrentValue;
        _logger = logger;
        _localizer = localizer;
        _emailMetadataRepository = emailMetadataRepository;
        _communicationTemplateRepository = communicationTemplateRepository;
        _communicationRepository = communicationRepository;
        _userRepository = userRepository;
        _rabbitMqService = rabbitMqService;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Processes and sends an outgoing email. Validates the request, saves communication data,
    /// sends the email, and updates the status. Returns a response based on the process outcome.
    /// </summary>
    /// <param name="sendEmailRequest">The details of the outgoing email request.</param>
    /// <returns>A <see cref="BaseResponse"/> indicating the result.</returns>

    public async Task<BaseResponse> OutgoingEmailHandlerAsync(OutgoingEmailRequest sendEmailRequest)
    {
        _logger.LogInformation("[OutgoingEmailHandler] Start process outgoing email handler");

        // Step 1. Validate the request
        sendEmailRequest.Sender = sendEmailRequest.EmailType == Constant.EmailType.NoReply ? _mailOptions.NoReply.Mail : _mailOptions.MailSupport.Mail;
        var (validationResult, sender, template) = await ValidateOutgoingEmailAsync(sendEmailRequest);
        if (validationResult.Status != StatusCodes.Status200OK)
        {
            return validationResult;
        }

        // Step 2. Save communication and related data
        var (saveOutgoingEmailResult, communication) = await SaveOutgoingEmailDataAsync(sendEmailRequest, sender!, template);
        if (!saveOutgoingEmailResult)
        {
            _logger.LogError("[OutgoingEmailHandlerAsync] An error has been occurred when saving communication data");
            return BaseResponse.ServerError();
        }

        // Step 3. Send the email
        try
        {
            var sendEmailResult = await SendEmailAsync(sendEmailRequest, template);
            if (!sendEmailResult)
            {
                await UpdateCommunicationStatusAsync(communication, Constant.CommunicationStatus.Failed);
                return BaseResponse.ServerError();
            }
        }
        catch (Exception)
        {
            await UpdateCommunicationStatusAsync(communication, Constant.CommunicationStatus.Failed);
            return BaseResponse.ServerError();
        }


        // Step 4. Update the email status
        await UpdateCommunicationStatusAsync(communication, Constant.CommunicationStatus.Delivered);
        return BaseResponse.Ok();
    }

    public Task<BaseResponse> QueueOutgoingEmailHandlerAsync(OutgoingEmailRequest sendEmailRequest)
    {
        _logger.LogInformation("[QueueOutgoingEmailHandlerAsync] Send outgoing email to queue");
        _rabbitMqService.InitializeQueue(_rabbitMqOptions.EmailQueue.OutgoingEmailQueue);
        _rabbitMqService.SendMessage(
            _rabbitMqOptions.EmailQueue.OutgoingEmailQueue,
            sendEmailRequest,
            _rabbitMqOptions.EmailQueue.OutgoingEmailQueue);

        return Task.FromResult(BaseResponse.Ok());
    }

    /// <summary>
    /// Handles the processing of an incoming email. This method is not yet implemented.
    /// </summary>
    /// <param name="emailRequest">The request object containing the details of the incoming email.</param>
    /// <returns>A <see cref="Task{BaseResponse}"/> indicating the result of the incoming email processing.</returns>
    /// <exception cref="NotImplementedException">Thrown to indicate that the method is not yet implemented.</exception>

    public Task<BaseResponse> IncomingEmailHandlerAsync(IncomingEmailRequest emailRequest)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Private Methods

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
    private async Task<(BaseResponse, User?, CommunicationTemplate?)> ValidateOutgoingEmailAsync(OutgoingEmailRequest sendEmailRequest)
    {
        var sender = await _userRepository.FindOneAsync(_ => _.Email == sendEmailRequest.Sender);
        if (sender is null)
        {
            _logger.LogError("[OutgoingEmailHandler] Not found or invalid sender {sender}", sendEmailRequest.Sender);
            return (BaseResponse.BadRequest(), null, null);
        }

        if (!string.IsNullOrEmpty(sendEmailRequest.TemplateName))
        {
            var template = await _communicationTemplateRepository.FindOneAsync(_ => _.TemplateName == sendEmailRequest.TemplateName);
            if (template is not null)
            {
                return (BaseResponse.Ok(), sender, template);
            }

            _logger.LogError("[OutgoingEmailHandler] Not found or invalid email template {templateName}", sendEmailRequest.TemplateName);
            return (BaseResponse.BadRequest(), sender, null);
        }

        _logger.LogInformation("[OutgoingEmailHandler] The email is sending without template");
        return (BaseResponse.Ok(), sender, null);
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
            Status = CommunicationStatus.Pending,
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
            throw new Exception("Email template can not be null or empty");
        }

        if (sendEmailRequest.TemplatePlaceHolders == null || !sendEmailRequest.TemplatePlaceHolders.Any())
        {
            return emailContent;
        }

        // Replace template placeholders in the email template
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
            _logger.LogInformation("[SendEmail] Start process to send email");

            // Create a new email message
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(emailRequest.Sender)
            };

            email.Subject = template?.CommunicationTemplateId > 0 ? template.Subject : emailRequest.Subject;
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
                throw new Exception("Email body can not be null or empty");
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
            _logger.LogInformation("[SendEmail] Smtp client connected");

            // Authenticate with the SMTP server
            await client.AuthenticateAsync(emailRequest.Sender, password).ConfigureAwait(false);

            // Send the email async
            await client.SendAsync(email).ConfigureAwait(false);

            // Disconnect from the SMTP server
            await client.DisconnectAsync(true).ConfigureAwait(false);
            _logger.LogInformation("[SendEmail] Sent email and disconnected successfully");

            return true;
        }
        catch (SmtpCommandException ex)
        {
            _logger.LogError($"[SendEmail] SMTP command error: {ex.Message}, StatusCode: {ex.StatusCode}");
            return false;
        }
        catch (SmtpProtocolException ex)
        {
            _logger.LogError($"[SendEmail] SMTP protocol error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError($"[SendEmail] {Helpers.BuildErrorMessage(ex)}");
            return false;
        }
    }

    #endregion
}