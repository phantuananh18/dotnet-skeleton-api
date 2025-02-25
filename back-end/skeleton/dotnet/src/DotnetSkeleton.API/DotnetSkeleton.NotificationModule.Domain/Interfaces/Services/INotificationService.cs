using DotnetSkeleton.NotificationModule.Domain.Model.Requests.MessageInstance;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;

namespace DotnetSkeleton.NotificationModule.Domain.Interfaces.Services
{
    public interface INotificationService
    {
        /// <summary>
        /// Send Notification to all client.
        /// </summary>
        /// <param name="request">The detail of notification to send.</param>
        /// <returns>An action result representing the result of send notification</returns>
        Task<BaseResponse> SendNotificationAsync(SendNotificationRequest request);
    }
}