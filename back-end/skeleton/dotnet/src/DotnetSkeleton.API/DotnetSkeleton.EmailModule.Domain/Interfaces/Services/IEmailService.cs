using DotnetSkeleton.EmailModule.Domain.Models.Requests;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;

namespace DotnetSkeleton.EmailModule.Domain.Interfaces.Services;

public interface IEmailService
{
    /// <summary>
    /// Sends an email based on the provided email request.
    /// </summary>
    /// <param name="emailRequest">An instance of the <see cref="OutgoingEmailRequest"/> class containing the necessary information for sending the email.</param>
    /// <returns>A task representing the asynchronous operation of sending the email.</returns>
    Task<BaseResponse> OutgoingEmailHandlerAsync(OutgoingEmailRequest emailRequest);

    /// <summary>
    /// Sends an email based on the provided email request to queue.
    /// </summary>
    /// <param name="sendEmailRequest">An instance of the <see cref="OutgoingEmailRequest"/> class containing the necessary information for sending the email.</param>
    /// <returns>A task representing the asynchronous operation of sending the email.</returns>
    Task<BaseResponse> QueueOutgoingEmailHandlerAsync(OutgoingEmailRequest sendEmailRequest);

    /// <summary>
    /// Handles incoming emails.
    /// </summary>
    /// <param name="emailRequest">An instance of the <see cref="IncomingEmailRequest"/> class containing the necessary information for incoming the email.</param>
    /// <returns>A task representing the asynchronous operation of incoming handler the email.</returns>
    Task<BaseResponse> IncomingEmailHandlerAsync(IncomingEmailRequest emailRequest);
}