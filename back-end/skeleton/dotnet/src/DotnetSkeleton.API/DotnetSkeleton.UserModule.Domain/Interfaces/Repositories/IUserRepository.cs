using DotnetSkeleton.SharedKernel.Utils.Models;
using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.UserModule.Domain.Model.Dtos.Users;

namespace DotnetSkeleton.UserModule.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Represents a repository for managing user entities.
    /// </summary>
    public interface IUserRepository : IBaseRepository<User, int>
    {
        Task<UserProfileData?> GetUserProfileDataByIdAsync(int userId);

        Task<UserInfo?> FindUserWithRoleByIdAsync(int userId);

        Task<List<UserPaginationDto>> GetAllUsersWithPaginationAsync(int pageNumber, int pageSize, string filter, string sort);

        Task<UserInfo?> FindUserInfoByIdAsync(int userId);
    }
}
