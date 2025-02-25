using DotnetSkeleton.Core.Domain.Models.Requests.Users;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;

namespace DotnetSkeleton.Core.Domain.Interfaces.Services
{
    /// <summary>
    /// Represents a service for managing user-related operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get a user by userId
        /// </summary>
        /// <param name="userId">An id of user</param>
        /// <returns> List of users </returns>
        Task<BaseResponse> GetUserByUserIdAsync(int userId);

        /// <summary>
        /// Create a new user with the provided information.
        /// </summary>
        /// <param name="request">The details of the user to create.</param>
        /// <returns>An action result representing the result of the user created process.</returns>
        Task<BaseResponse> CreateUserAsync(CreateUserRequest request);

        /// <summary>
        /// Update a user with the provided information.
        /// </summary>
        /// <param name="userId">The User ID</param>
        /// <param name="request">The details of the user to update.</param>
        /// <returns>An action result representing the result of the user updated process.</returns>
        Task<BaseResponse> UpdateUserAsync(int userId, UpdateUserRequest request);

        /// <summary>
        /// Delete a existing user (sort delete)
        /// </summary>
        /// <param name="userId">An id of user</param>
        /// <returns>An action result representing the result of the user deleted process.</returns>
        Task<BaseResponse> DeleteUserAsync(int userId);

        /// <summary>
        /// Retrieves all users with pagination based on the specified request parameters.
        /// </summary>
        /// <param name="request">The request containing filtering, sorting, and pagination information for retrieving users.</param>
        /// <returns>A <see cref="BaseResponse"/> containing the result of the operation, including user data or an error if the operation fails.</returns>
        Task<BaseResponse> GetAllUsersWithPaginationAsync(GetAllUsersRequest request);
    }
}