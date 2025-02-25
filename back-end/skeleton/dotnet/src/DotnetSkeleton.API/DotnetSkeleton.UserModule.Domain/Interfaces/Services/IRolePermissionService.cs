using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Model.Requests.RolePermissions;

namespace DotnetSkeleton.UserModule.Domain.Interfaces.Services
{
    public interface IRolePermissionService
    {
        /// <summary>
        /// Create a new rolePermission with the provided information.
        /// </summary>
        /// <param name="request">The details of the rolePermission to create.</param>
        /// <returns>An action result representing the result of the rolePermission created process.</returns>
        Task<BaseResponse> CreateRolePermissionAsync(CreateRolePermissionRequest request);

        /// <summary>
        /// Update a rolePermission with the provided information.
        /// </summary>
        /// <param name="rolePermissionId">The RolePermission ID</param>
        /// <param name="request">The details of the rolePermission to update.</param>
        /// <returns>An action result representing the result of the rolePermission updated process.</returns>
        Task<BaseResponse> UpdatedRolePermissionAsync(int rolePermissionId, UpdateRolePermissionRequest request);

        /// <summary>
        /// Delete a existing rolePermission (sort delete)
        /// </summary>
        /// <param name="rolePermissionId">An id of rolePermission</param>
        /// <returns>An action result representing the result of the rolePermission deleted process.</returns>
        Task<BaseResponse> DeleteRolePermissionAsync(int rolePermissionId);

        /// <summary>
        /// Assign the role permissions with the provided information.
        /// </summary>
        /// <param name="request">The details of the role permissions to assign.</param>
        /// <returns>An action result representing the result of the role permissions assigned process.</returns>
        Task<BaseResponse> AssignRolePermissionsAsync(AssignRolePermissionRequest request);
    }
}
