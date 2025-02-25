using DotnetSkeleton.IdentityModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.IdentityModule.Domain.Models.Dto;

namespace DotnetSkeleton.IdentityModule.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Represents a repository for managing user entities.
    /// </summary>
    public interface IUserRepository : IBaseRepository<User, int>
    {
        /// <summary>
        /// Finds a user and related data based on the specified criteria.
        /// </summary>
        /// <param name="criteriaName">The name of the criteria used for searching.</param>
        /// <param name="criteriaValue">The value of the criteria used for searching.</param>
        /// <returns>The task result contains an instance of <see cref="UserAndRelatedData"/> if found, otherwise null.</returns>
        Task<UserAndRelatedData?> FindUserAndRelatedDataByCriteria(string criteriaName, string criteriaValue);

        /// <summary>
        /// Retrieves user information by email.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>The task result contains an instance of <see cref="UserInfo"/> if found, otherwise null.</returns>
        Task<UserInfo?> GetUserInfoByEmail(string email);
    }
}