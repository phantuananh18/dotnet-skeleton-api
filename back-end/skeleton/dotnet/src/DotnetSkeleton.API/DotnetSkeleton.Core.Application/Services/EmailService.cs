using DotnetSkeleton.Core.Domain.Interfaces.Services;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using DotnetSkeleton.SharedKernel.Utils.Models.Requests;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OutgoingEmailRequest = DotnetSkeleton.Core.Domain.Models.Requests.EmailRequests.OutgoingEmailRequest;

namespace DotnetSkeleton.Core.Application.Services
{
    public class EmailService : IEmailService
    {
        #region Private Fields
        private readonly ILogger<EmailService> _logger;
        private readonly SystemInfoOptions _systemInfoOptions;

        #endregion

        #region Constructor
        public EmailService(ILogger<EmailService> logger, IOptionsMonitor<SystemInfoOptions> systemInfoOptions)
        {
            _logger = logger;
            _systemInfoOptions = systemInfoOptions.CurrentValue;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Processes and sends an outgoing email. Validates the request, saves communication data, sends the email, and updates the status. Returns a response based on the process outcome.
        /// </summary>
        /// <param name="sendEmailRequest">The details of the outgoing email request.</param>
        /// <returns>A <see cref="BaseResponse"/> indicating the result.</returns>
        public async Task<BaseResponse> SendOutgoingEmail(OutgoingEmailRequest sendEmailRequest)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Post,
                BaseUrl = _systemInfoOptions.EmailServiceUrl!,
                Endpoint = Constant.ApiEndpoints.EmailEndpoints.SendMail,
                RequestSource = Constant.ServiceName.CoreService,
                Body = sendEmailRequest
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Sends an email based on the provided email request to queue.
        /// </summary>
        /// <param name="sendEmailRequest">An instance of the <see cref="OutgoingEmailRequest"/> class containing the necessary information for sending the email.</param>
        /// <returns>A task representing the asynchronous operation of sending the email.</returns>
        public async Task<BaseResponse> QueueOutgoingEmail(OutgoingEmailRequest sendEmailRequest)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Post,
                BaseUrl = _systemInfoOptions.EmailServiceUrl!,
                Endpoint = Constant.ApiEndpoints.EmailEndpoints.QueueMail,
                RequestSource = Constant.ServiceName.CoreService,
                Body = sendEmailRequest
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        #endregion
    }
}