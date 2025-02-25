using DotnetSkeleton.NotificationModule.Domain.Entities.MySQLEntities;

namespace DotnetSkeleton.NotificationModule.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Represents a repository for managing <see cref="NotificationType"/> notification entities.
    /// </summary>
    public interface INotificationTypeRepository : IBaseRepository<NotificationType, int>
    {
    }
}