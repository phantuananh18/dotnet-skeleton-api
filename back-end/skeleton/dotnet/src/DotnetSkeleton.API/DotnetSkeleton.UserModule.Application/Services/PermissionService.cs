using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.UserModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using DotnetSkeleton.UserModule.Domain.Model.Responses.PermissionResponses;
using DotnetSkeleton.UserModule.Domain.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.UserModule.Domain.Model.Requests.Permissions;
using DotnetSkeleton.UserModule.Domain.Model.Utils;

namespace DotnetSkeleton.UserModule.Application.Services
{
    public class PermissionService : IPermissionService
    {
        #region Private Fields
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IFeatureRepository _featureRepository;
        private readonly IStringLocalizer<Resources> _localizer;
        private readonly ILogger<PermissionService> _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Contructor
        public PermissionService(IPermissionRepository permissionRepository,
            IRoleRepository roleRepository,
            IFeatureRepository featureRepository,
            ILogger<PermissionService> logger,
            IMapper mapper,
            IStringLocalizer<Resources> localizer)
        {
            _permissionRepository = permissionRepository ?? throw new ArgumentNullException(nameof(permissionRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _featureRepository = featureRepository ?? throw new ArgumentNullException(nameof(featureRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Get a permission by permissionId
        /// </summary>
        /// <param name="permissionId">An id of permission</param>
        /// <returns> List of permissions </returns>
        public async Task<BaseResponse> GetPermissionByPermissionIdAsync(int permissionId)
        {
            var permission = await _permissionRepository.FindByIdAsync(permissionId);
            return permission != null
                ? BaseResponse.Ok(permission)
                : BaseResponse.NotFound();
        }

        /// <summary>
        /// Get all permissions
        /// </summary>
        /// <param name="request">The request containing filtering, sorting, and pagination information for retrieving permissions.</param>
        /// <returns>A <see cref="BaseResponse"/> containing the result of the operation, including permission data or an error if the operation fails.</returns>
        public async Task<BaseResponse> GetAllPermissionsAsync(GetAllPermissionsRequest request)
        {
            _logger.LogInformation($"[GetAllPermissionsAsync] Start to get all users with pagination. Request: {JsonSerializer.Serialize(request)}");

            var role = await _roleRepository.FindOneAsync(_ => _.Name == request.RoleName);
            if (role == null)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_Role_Not_Found)]);
            }

            string roleFilter = string.Format(Constant.RoleSearchKeyword.SearchRoleId, role.RoleId);

            // Refer ticket DWF-105
            // Only one column can be sorted at a time
            // Default sort by name ascending
            string sort = string.Empty;
            var permissionSortColumm = new PermissionSortColumnMapping();
            if (request.Sort != null && request.Sort.Length == 1)
            {
                sort = Helpers.SortStringGenerator(request.Sort, permissionSortColumm);
            }
            else
            {
                sort = $"{permissionSortColumm.Permission}";
            }

            var permissions = await _permissionRepository.GetAllPermissionWithPaginationAsync(roleFilter, sort);

            // Group results by FeatureId and transform into desired format
            var result = permissions
                .GroupBy(r => new { r.FeatureId, r.FeatureName , r.FeatureDescription})
                .Select(g => new PermissionPaginationResponse
                {
                    FeatureId = g.Key.FeatureId,
                    FeatureName = g.Key.FeatureName,
                    FeatureDescription = g.Key.FeatureDescription,
                    Permissions = g.Select(p => new PermissionDetailResponse()
                    {
                        PermissionId = p.PermissionId,
                        PermissionName = p.PermissionName,
                        Allowed = p.Allow
                    }).ToList()
                }).ToList();

            int totalRecords = result.Count > 0 ? result.Count : 0;
            int totalPages = (int)Math.Ceiling((double)totalRecords / request.PageSize);

            return BaseResponse.Ok(new PaginationBaseResult<List<PermissionPaginationResponse>>
            {
                Results = result,
                TotalNumberOfRecords = totalRecords,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = totalPages
            });
        }

        /// <summary>
        /// Create a new permission with the provided information.
        /// </summary>
        /// <param name="request">The details of the permission to create.</param>
        /// <returns>An action result representing the result of the permission created process.</returns>
        public async Task<BaseResponse> CreatePermissionAsync(CreatePermissionRequest request)
        {
            var validateRolePermissionResult = await ValidatePermissionInfoAsync(request);
            if (validateRolePermissionResult.Status != (int)HttpStatusCode.OK)
            {
                return validateRolePermissionResult;
            }

            var permissionEntity = _mapper.Map<Permission>(request);
            var newPermission = await _permissionRepository.AddAsync(permissionEntity);

            return BaseResponse.Ok(_mapper.Map<PermissionResponse>(newPermission));
        }

        /// <summary>
        /// Update a permission with the provided information.
        /// </summary>
        /// <param name="permissionId">The Permission ID</param>
        /// <param name="request">The details of the permission to update.</param>
        /// <returns>An action result representing the result of the permission updated process.</returns>
        public async Task<BaseResponse> UpdatedPermissionAsync(int permissionId, UpdatePermissionRequest request)
        {
            var permission = await _permissionRepository.FindByIdAsync(permissionId);
            if (permission == null || permission.IsDeleted)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_Permission_Not_Found)]);
            }

            permission.Name = request.Name;
            permission.Code = request.Code;
            permission.Description = request.Description;
            var updatedPermission = await _permissionRepository.UpdateAsync(permission);

            return BaseResponse.Ok(_mapper.Map<PermissionResponse>(updatedPermission));
        }

        /// <summary>
        /// Delete a existing permission (sort delete)
        /// </summary>
        /// <param name="permissionId">An id of permission</param>
        /// <returns>An action result representing the result of the permission deleted process.</returns>
        public async Task<BaseResponse> DeletePermissionAsync(int permissionId)
        {
            var permission = await _permissionRepository.FindByIdAsync(permissionId);
            if (permission == null || permission.IsDeleted)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_Permission_Not_Found)]);
            }

            permission.IsDeleted = true;
            var deletedPermission = await _permissionRepository.UpdateAsync(permission);

            return BaseResponse.Ok(new { deletedPermission.PermissionId });
        }
        #endregion

        #region Private Methods

        private async Task<BaseResponse> ValidatePermissionInfoAsync(CreatePermissionRequest request)
        {
            var existingFeature = await _featureRepository.FindOneAsync(x => x.FeatureId == request.FeatureId && x.IsDeleted == false);
            if (existingFeature == null)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_Feature_Not_Found)]);
            }
            
            var existingRolePermission = await _permissionRepository
                .FindOneAsync(x => x.Name == request.Name || x.Code == request.Code);

            if (existingRolePermission != null)
            {
                if (existingRolePermission.IsDeleted)
                {
                    return BaseResponse.BadRequest(_localizer[nameof(Resources.This_RolePermission_Has_Been_Deactivated)]);
                }

                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_RolePermission_Already_Exists)]);
            }

            return BaseResponse.Ok();
        }

        #endregion
    }
}
