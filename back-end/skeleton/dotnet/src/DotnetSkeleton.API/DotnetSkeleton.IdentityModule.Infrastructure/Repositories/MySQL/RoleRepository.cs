using DotnetSkeleton.IdentityModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.IdentityModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.IdentityModule.Infrastructure.DbContexts;

namespace DotnetSkeleton.IdentityModule.Infrastructure.Repositories.MySQL
{
    /// <summary>
    /// Role repository
    /// </summary>
    public class RoleRepository(SkeletonDbContext context) : BaseRepository<Role, int>(context), IRoleRepository
    {
        // TO-DO: Implement custom methods for RoleRepository
    }
}