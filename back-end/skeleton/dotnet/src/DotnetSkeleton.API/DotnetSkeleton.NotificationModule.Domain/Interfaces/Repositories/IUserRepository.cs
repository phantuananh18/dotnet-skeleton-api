using DotnetSkeleton.NotificationModule.Domain.Entities.MySQLEntities;

namespace DotnetSkeleton.NotificationModule.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Represents a repository for managing <see cref="User"/> entities in the MySQL database.
    /// </summary>
    public interface IUserRepository : IBaseRepository<User, int>
    {
    }
}