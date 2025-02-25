using DotnetSkeleton.NotificationModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.NotificationModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.NotificationModule.Infrastructure.DbContexts;

namespace DotnetSkeleton.NotificationModule.Infrastructure.Repositories.MySQL
{
    /// <summary>
    /// Represents a repository for managing <see cref="User"/> entities in the MySQL database.
    /// </summary>
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        private readonly SkeletonDbContext _dbContext;

        public UserRepository(SkeletonDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}