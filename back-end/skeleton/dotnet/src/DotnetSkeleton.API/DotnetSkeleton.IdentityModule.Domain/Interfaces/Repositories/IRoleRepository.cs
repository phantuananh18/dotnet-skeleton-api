using DotnetSkeleton.IdentityModule.Domain.Entities.MySQLEntities;

namespace DotnetSkeleton.IdentityModule.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Represents a repository for managing role entities.
    /// </summary>
    public interface IRoleRepository : IBaseRepository<Role, int>
    {
    }
}