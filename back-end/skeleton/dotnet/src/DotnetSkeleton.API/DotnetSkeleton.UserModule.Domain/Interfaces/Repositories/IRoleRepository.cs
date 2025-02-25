using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.UserModule.Domain.Model.Dtos.Roles;

namespace DotnetSkeleton.UserModule.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Represents a repository for managing role entities.
    /// </summary>
    public interface IRoleRepository : IBaseRepository<Role, int>
    {
        Task<List<RolePaginationDto>> GetAllRolesWithPaginationAsync(int pageNumber, int pageSize, string searchText, string sort);
    }
}
