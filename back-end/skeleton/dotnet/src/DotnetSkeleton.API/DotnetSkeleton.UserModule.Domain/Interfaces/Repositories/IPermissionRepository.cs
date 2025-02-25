using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.UserModule.Domain.Model.Dtos.Permission;
using DotnetSkeleton.UserModule.Domain.Model.Dtos.Users;

namespace DotnetSkeleton.UserModule.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Represents a repository for managing permisstion entities.
    /// </summary>
    public interface IPermissionRepository : IBaseRepository<Permission, int>
    {
        Task<List<PermissionPaginationDto>> GetAllPermissionWithPaginationAsync(string roleFilter, string sort);
    }
}
