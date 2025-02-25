using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.UserModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.UserModule.Infrastructure.DbContexts;

namespace DotnetSkeleton.UserModule.Infrastructure.Repositories.MySQL
{
    public class RolePermissionRepository(SkeletonDbContext context) : BaseRepository<RolePermission, int>(context), IRolePermissionRepository
    {
    }
}
