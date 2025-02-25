using DotnetSkeleton.Core.Domain.Models.Requests.RolePermissions;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;

namespace DotnetSkeleton.Core.Domain.Interfaces.Services
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
        /// <param name="request">The details of the rolePermission to update.</param>
        /// <returns>An action result representing the result of the rolePermission updated process.</returns>
        Task<BaseResponse> UpdatedRolePermissionAsync(UpdateRolePermissionRequest request);

        /// <summary>
        /// Delete a existing rolePermission (sort delete)
        /// </summary>
        /// <param name="rolePermissionId">An id of rolePermission</param>
        /// <returns>An action result representing the result of the rolePermission deleted process.</returns>
        Task<BaseResponse> DeleteRolePermissionAsync(int rolePermissionId);
    }
}