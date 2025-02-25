using DotnetSkeleton.Core.Domain.Models.Requests.EmailRequests;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;

namespace DotnetSkeleton.Core.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email based on the provided email request.
        /// </summary>
        /// <param name="emailRequest">An instance of the <see cref="OutgoingEmailRequest"/> class containing the necessary information for sending the email.</param>
        /// <returns>A task representing the asynchronous operation of sending the email.</returns>
        Task<BaseResponse> SendOutgoingEmail(OutgoingEmailRequest emailRequest);

        /// <summary>
        /// Sends an email based on the provided email request to queue.
        /// </summary>
        /// <param name="sendEmailRequest">An instance of the <see cref="OutgoingEmailRequest"/> class containing the necessary information for sending the email.</param>
        /// <returns>A task representing the asynchronous operation of sending the email.</returns>
        Task<BaseResponse> QueueOutgoingEmail(OutgoingEmailRequest sendEmailRequest);
    }
}