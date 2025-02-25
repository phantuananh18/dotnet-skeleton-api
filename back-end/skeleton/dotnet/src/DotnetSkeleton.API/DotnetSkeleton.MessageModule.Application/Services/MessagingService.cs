using DotnetSkeleton.MessageModule.Domain.Interfaces.Services;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Verify.V2.Service;
using Twilio.Types;

namespace DotnetSkeleton.MessageModule.Application.Services
{
    public class MessagingService : IMessagingService
    {
        private readonly TwilioOptions _twilioOptions;
        private readonly ILogger<MessagingService> _logger;
        public MessagingService(IOptions<TwilioOptions> twilioOptions, ILogger<MessagingService> logger)
        {
            _twilioOptions = twilioOptions.Value;
            TwilioClient.Init(_twilioOptions.AccountSID, _twilioOptions.AuthToken);
            _logger = logger;
        }

        /// <summary>
        /// Sends an SMS to the specified phone number using Twilio's messaging service.
        /// </summary>
        /// <param name="ToPhoneNumber">The destination phone number in E.164 format (e.g., +1234567890).</param>
        /// <param name="message">The message body to be sent.</param>
        /// <returns>
        /// A <see cref="BaseResponse"/> containing the response from the Twilio service, 
        /// indicating whether the message was successfully sent.
        /// </returns>
        /// <remarks>
        /// This method uses Twilio's API to send SMS messages. It logs the initiation and 
        /// completion of the SMS sending process, as well as any failures.
        /// </remarks>
        public async Task<BaseResponse> SendSmsAsync(string ToPhoneNumber, string message)
        {
            _logger.LogInformation("Initiating SMS sending process");

            var msg = await MessageResource.CreateAsync(
                body: message,
                from: new PhoneNumber(_twilioOptions.FromPhoneNumber),
                to: new PhoneNumber(ToPhoneNumber));

            if (msg.Status == MessageResource.StatusEnum.Queued
                || msg.Status == MessageResource.StatusEnum.Sent
                || msg.Status == MessageResource.StatusEnum.Delivered)
            {
                _logger.LogInformation("SMS sent successfully with Message SID: {MessageSid}", msg.Sid);
                return BaseResponse.Ok("SMS sent successfully.");
            }

            return BaseResponse.BadRequest("Failed to send SMS with Message SID: {MessageSid}", msg.Sid);
        }

        /// <summary>
        /// Initiates a verification process for the specified phone number using the provided channel (e.g., SMS, call).
        /// </summary>
        /// <param name="phoneNumber">The phone number to which the verification should be sent.</param>
        /// <param name="channel">The communication channel to use for verification (e.g., "sms" or "call").</param>
        public async Task<BaseResponse> StartVerificationAsync(string phoneNumber, string channel)
        {
            _logger.LogInformation("Starting verification using {Channel} channel.", channel);
            var verificationResource = await VerificationResource.CreateAsync(
                to: phoneNumber,
                channel: channel,
                pathServiceSid: _twilioOptions.VerificationSid
            );

            _logger.LogInformation("Verification initiated successfully with Verification SID: {VerificationSid}", verificationResource.Sid);
            return BaseResponse.Ok(verificationResource);
        }

        /// <summary>
        /// Checks the verification status for a given phone number and code.
        /// </summary>
        /// <param name="phoneNumber">The phone number associated with the verification.</param>
        /// <param name="code">The verification code provided by the user.</param>
        /// <returns>A <see cref="BaseResponse"/> object indicating the result of the verification check.</returns>
        public async Task<BaseResponse> CheckVerificationAsync(string phoneNumber, string code)
        {
            _logger.LogInformation("Checking verification for phone number with provided code.");
            var verificationCheckResource = await VerificationCheckResource.CreateAsync(
                to: phoneNumber,
                code: code,
                pathServiceSid: _twilioOptions.VerificationSid
            );

            if (verificationCheckResource.Status.Equals("approved"))
            {
                _logger.LogInformation("Verification approved for phone number with Verification SID. {VerificationSid}", verificationCheckResource.Sid);
                return BaseResponse.Ok(verificationCheckResource);
            }

            _logger.LogWarning("Verification failed for phone number. Wrong code provided.");
            return BaseResponse.BadRequest("Wrong code. Try again.");
        }
    }
}
