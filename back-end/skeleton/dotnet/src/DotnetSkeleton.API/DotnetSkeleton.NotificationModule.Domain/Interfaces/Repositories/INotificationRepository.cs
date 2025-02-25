using DotnetSkeleton.NotificationModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.NotificationModule.Domain.Model.Requests.RecordNotification;

namespace DotnetSkeleton.NotificationModule.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Represents a repository for managing notification entities.
    /// </summary>
    public interface INotificationRepository : IBaseRepository<Notification, long>
    {
        Task<int> RecordNotificationAsync(RecordNotificationRequest request);
    }
}