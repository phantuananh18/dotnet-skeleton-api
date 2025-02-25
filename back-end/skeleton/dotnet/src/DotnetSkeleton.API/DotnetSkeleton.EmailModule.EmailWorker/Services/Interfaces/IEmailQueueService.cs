using DotnetSkeleton.EmailModule.Domain.Models.Requests;

namespace DotnetSkeleton.EmailModule.EmailWorker.Services.Interfaces;

public interface IEmailQueueService
{
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
    Task<bool> OutgoingEmailHandlerAsync(OutgoingEmailRequest email);
}