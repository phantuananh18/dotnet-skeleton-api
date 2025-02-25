using DotnetSkeleton.Core.Domain.Models.Requests.Messages;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;

namespace DotnetSkeleton.Core.Domain.Interfaces.Services
{
    public interface IMessageService
    {
        /// <summary>
        /// Handles the HTTP POST request to send an SMS message.
        /// </summary>
        /// <param name="request">The <see cref="SendSmsRequest"/> containing the phone number and message details.</param>
        /// <returns>
        /// An <see cref="BaseResponse"/> that represents the result of the SMS sending operation.
        /// The status code reflects the success or failure of the process.
        /// </returns>
        /// <remarks>
        /// This method receives a command with the required details for sending an SMS, such as the recipient's phone number and message.
        /// It forwards the command to the mediator for processing and returns the result, including an appropriate HTTP status code.
        /// </remarks>
        Task<BaseResponse> SendSmsAsync(SendSmsRequest request);

        /// <summary>
        /// Initiates a verification process for the current user via the specified communication channel.
        /// </summary>
        /// <param name="request">The request containing the channel to be used for verification.</param>
        /// <returns>
        /// An <see cref="BaseResponse"/> that represents the result of the verification initiation request.
        /// The status code corresponds to the outcome of the verification process.
        /// </returns>
        /// <remarks>
        /// This method retrieves the current user's data from the HTTP context, initiates a verification process 
        /// using the provided mobile phone number and channel, and returns the result. 
        /// The response status code will reflect the success or failure of the operation.
        /// </remarks>
        Task<BaseResponse> StartVerificationAsync(StartVerificationRequest request);

        /// <summary>
        /// Verifies the provided code for the current user's mobile phone number.
        /// </summary>
        /// <param name="request">The request containing the verification code to be checked.</param>
        /// <returns>
        /// An <see cref="BaseResponse"/> that represents the result of the verification check.
        /// The status code corresponds to the outcome of the verification process.
        /// </returns>
        /// <remarks>
        /// This method retrieves the current user's data from the HTTP context and attempts to verify the provided code 
        /// against the user's mobile phone number. The response status code will indicate whether the verification 
        /// was successful or if it failed.
        /// </remarks>
        Task<BaseResponse> CheckVerificationAsync(CheckVerificationRequest request);
    }
}