using DotnetSkeleton.Core.Domain.Models.Requests.RolePermissions;
using DotnetSkeleton.Core.Domain.Models.Requests.Roles;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;

namespace DotnetSkeleton.Core.Domain.Interfaces.Services
{
    public interface IRoleService
    {
        /// <summary>
        /// Get a role by roleId
        /// </summary>
        /// <param name="roleId">An id of role</param>
        /// <returns> List of roles </returns>
        Task<BaseResponse> GetRoleByRoleIdAsync(int roleId);

        /// <summary>
        /// Create a new role with the provided information.
        /// </summary>
        /// <param name="request">The details of the role to create.</param>
        /// <returns>An action result representing the result of the role created process.</returns>
        Task<BaseResponse> CreateRoleAsync(CreateRoleRequest request);

        /// <summary>
        /// Update a role with the provided information.
        /// </summary>
        /// <param name="request">The details of the role to update.</param>
        /// <returns>An action result representing the result of the role updated process.</returns>
        Task<BaseResponse> UpdatedRoleAsync(UpdateRoleRequest request);

        /// <summary>
        /// Delete a existing role (sort delete)
        /// </summary>
        /// <param name="roleId">An id of role</param>
        /// <returns>An action result representing the result of the role deleted process.</returns>
        Task<BaseResponse> DeleteRoleAsync(int roleId);

        /// <summary>
        /// Retrieves all roles with pagination based on the specified request parameters.
        /// </summary>
        /// <param name="request">The request containing searching, sorting, and pagination information for retrieving roles.</param>
        /// <returns>A <see cref="BaseResponse"/> containing the result of the operation, including role data or an error if the operation fails.</returns>
        Task<BaseResponse> GetAllRolesWithPaginationAsync(GetAllRolesRequest request);

        /// <summary>
        /// Assign the role permissions with the provided information.
        /// </summary>
        /// <param name="roleId">The Role ID</param>
        /// <param name="request">The details of the role permissions to assign.</param>
        /// <returns>An action result representing the result of the role permissions assigned process.</returns>
        Task<BaseResponse> AssignRolePermissionsAsync(int roleId, AssignRolePermissionRequest request);
    }
}