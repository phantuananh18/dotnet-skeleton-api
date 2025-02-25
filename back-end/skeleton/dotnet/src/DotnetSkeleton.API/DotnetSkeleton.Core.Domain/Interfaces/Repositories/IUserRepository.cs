using DotnetSkeleton.Core.Domain.Entities.MySQLEntities;
using DotnetSkeleton.SharedKernel.Utils.Models;

namespace DotnetSkeleton.Core.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Represents a repository for managing user entities.
    /// </summary>
    public interface IUserRepository : IBaseRepository<User, int>
    {
        Task<UserProfileData?> GetUserProfileDataByIdAsync(int userId);
    }
}