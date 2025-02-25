using DotnetSkeleton.NotificationModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.NotificationModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.NotificationModule.Infrastructure.DbContexts;

namespace DotnetSkeleton.NotificationModule.Infrastructure.Repositories.MySQL
{
    /// <summary>
    /// Represents a repository for managing <see cref="NotificationType"/> entities in the MySQL database.
    /// </summary>
    public class NotificationTypeRepository : BaseRepository<NotificationType, int>, INotificationTypeRepository
    {
        private readonly SkeletonDbContext _context;

        public NotificationTypeRepository(SkeletonDbContext context) : base(context)
        {
            _context = context;
        }
    }
}