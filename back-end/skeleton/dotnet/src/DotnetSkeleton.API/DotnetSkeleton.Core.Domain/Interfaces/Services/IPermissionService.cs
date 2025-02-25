using DotnetSkeleton.Core.Domain.Models.Requests.Permissions;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;

namespace DotnetSkeleton.Core.Domain.Interfaces.Services
{
    public interface IPermissionService
    {
        /// <summary>
        /// Get a permission by permissionId
        /// </summary>
        /// <param name="permissionId">An id of permission</param>
        /// <returns> List of permissions </returns>
        Task<BaseResponse> GetPermissionByPermissionIdAsync(int permissionId);

        /// <summary>
        /// Create a new permission with the provided information.
        /// </summary>
        /// <param name="request">The details of the permission to create.</param>
        /// <returns>An action result representing the result of the permission created process.</returns>
        Task<BaseResponse> CreatePermissionAsync(CreatePermissionRequest request);

        /// <summary>
        /// Update a permission with the provided information.
        /// </summary>
        /// <param name="request">The details of the permission to update.</param>
        /// <returns>An action result representing the result of the permission updated process.</returns>
        Task<BaseResponse> UpdatedPermissionAsync(UpdatePermissionRequest request);

        /// <summary>
        /// Delete a existing permission (sort delete)
        /// </summary>
        /// <param name="permissionId">An id of permission</param>
        /// <returns>An action result representing the result of the permission deleted process.</returns>
        Task<BaseResponse> DeletePermissionAsync(int permissionId);

        /// <summary>
        /// Get all permissions
        /// </summary>
        /// <param name="request">The request containing sorting, and pagination information for retrieving permissions.</param>
        /// <returns>A <see cref="BaseResponse"/> containing the result of the operation, including permission data or an error if the operation fails.</returns>
        Task<BaseResponse> GetAllPermissionsAsync(GetAllPermissionsRequest request);
    }
}