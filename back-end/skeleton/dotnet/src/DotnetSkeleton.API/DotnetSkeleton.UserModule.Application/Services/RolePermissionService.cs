using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.UserModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using DotnetSkeleton.UserModule.Domain.Model.Responses.RolePermissionResponses;
using DotnetSkeleton.UserModule.Domain.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using DotnetSkeleton.UserModule.Domain.Model.Requests.RolePermissions;

namespace DotnetSkeleton.UserModule.Application.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        #region Private Fields
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IStringLocalizer<Resources> _localizer;
        private readonly ILogger<RolePermissionService> _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Contructor
        public RolePermissionService(IRolePermissionRepository rolePermissionRepository,
            ILogger<RolePermissionService> logger,
            IMapper mapper,
            IStringLocalizer<Resources> localizer,
            IRoleRepository roleRepository,
            IPermissionRepository permissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository ?? throw new ArgumentNullException(nameof(rolePermissionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Create a new rolePermission with the provided information.
        /// </summary>
        /// <param name="request">The details of the rolePermission to create.</param>
        /// <returns>An action result representing the result of the rolePermission created process.</returns>
        public async Task<BaseResponse> CreateRolePermissionAsync(CreateRolePermissionRequest request)
        {
            var validateRolePermissionResult = await ValidateRolePermissionInfoAsync(request);
            if (validateRolePermissionResult.Status != (int)HttpStatusCode.OK)
            {
                return validateRolePermissionResult;
            }

            var rolePermissionEntity = _mapper.Map<RolePermission>(request);
            var newRolePermission = await _rolePermissionRepository.AddAsync(rolePermissionEntity);

            return BaseResponse.Ok(_mapper.Map<RolePermissionResponse>(newRolePermission));
        }

        /// <summary>
        /// Update a rolePermission with the provided information.
        /// </summary>
        /// <param name="rolePermissionId">The RolePermission ID</param>
        /// <param name="request">The details of the rolePermission to update.</param>
        /// <returns>An action result representing the result of the rolePermission updated process.</returns>
        public async Task<BaseResponse> UpdatedRolePermissionAsync(int rolePermissionId, UpdateRolePermissionRequest request)
        {
            var rolePermission = await _rolePermissionRepository.FindByIdAsync(rolePermissionId);
            if (rolePermission == null || rolePermission.IsDeleted)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_RolePermission_Not_Found)]);
            }

            rolePermission.RoleId = request.RoleId;
            rolePermission.PermissionId = request.PermissionId;
            var updatedRolePermission = await _rolePermissionRepository.UpdateAsync(rolePermission);

            return BaseResponse.Ok(_mapper.Map<RolePermissionResponse>(updatedRolePermission));
        }

        /// <summary>
        /// Delete a existing rolePermission (sort delete)
        /// </summary>
        /// <param name="rolePermissionId">An id of rolePermission</param>
        /// <returns>An action result representing the result of the rolePermission deleted process.</returns>
        public async Task<BaseResponse> DeleteRolePermissionAsync(int rolePermissionId)
        {
            var rolePermission = await _rolePermissionRepository.FindByIdAsync(rolePermissionId);
            if (rolePermission == null || rolePermission.IsDeleted)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_RolePermission_Not_Found)]);
            }

            rolePermission.IsDeleted = true;
            var deletedRolePermission = await _rolePermissionRepository.UpdateAsync(rolePermission);

            return BaseResponse.Ok(new { deletedRolePermission.RolePermissionId });
        }

        public async Task<BaseResponse> AssignRolePermissionsAsync(AssignRolePermissionRequest request)
        {
            var (validateRequest, existingPermissions, existingRolePermissions) = await ValidateAssignRolePermissionRequestAsync(request);
            if (validateRequest.Status != StatusCodes.Status200OK)
            {
                return validateRequest;
            }

            _logger.LogInformation($"[AssignRolePermissionsAsync] - Start assign role permissions. Request: {JsonSerializer.Serialize(request)}");
            
            var newPermissions = request.Permissions.Where(_ => existingRolePermissions!.All(e => e.PermissionId != _.PermissionId)).ToList();
            if (newPermissions != null && newPermissions.Count > 0)
            {
                var insertNewRolePermissions = _mapper.Map<List<RolePermission>>(newPermissions);
                insertNewRolePermissions.ForEach(r =>
                {
                    r.RoleId = request.RoleId;
                });

                await _rolePermissionRepository.BulkInsertAsync(insertNewRolePermissions);
            }

            List<RolePermission> updateRolePermissions = new List<RolePermission>();
            foreach (var existingRolePermission in existingRolePermissions!)
            {
                var permission = request.Permissions.FirstOrDefault(_ => _.PermissionId == existingRolePermission!.PermissionId);
                if (permission != null && permission.Allowed == existingRolePermission!.IsDeleted)
                {
                    existingRolePermission.IsDeleted = !permission.Allowed;
                    updateRolePermissions.Add(existingRolePermission!);
                }
            }

            if (updateRolePermissions != null && updateRolePermissions.Count > 0)
            {
                await _rolePermissionRepository.BulkUpdateAsync(updateRolePermissions);
            }

            return BaseResponse.Ok();
        }

        #endregion

        #region Private Methods

        private async Task<BaseResponse> ValidateRolePermissionInfoAsync(CreateRolePermissionRequest request)
        {
            var existingRole = await _roleRepository.FindOneAsync(x => x.RoleId == request.RoleId);

            if (existingRole == null || existingRole.IsDeleted)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_Role_Not_Found)]);
            }

            var existingPermission = await _permissionRepository.FindOneAsync(x => x.PermissionId == request.PermissionId);

            if (existingPermission == null || existingPermission.IsDeleted)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_Permission_Not_Found)]);
            }

            var existingRolePermission = await _rolePermissionRepository
                .FindOneAsync(x => x.RoleId == request.RoleId && x.PermissionId == request.PermissionId);

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

        private async Task<(BaseResponse, List<Permission>?, List<RolePermission>?)> ValidateAssignRolePermissionRequestAsync(AssignRolePermissionRequest request)
        {
            List<int> permissionIds = request.Permissions.Select(_ => _.PermissionId).ToList();
            var existingRole = await _roleRepository.FindByIdAsync(request.RoleId);
            if (existingRole == null)
            {
                return (BaseResponse.NotFound(_localizer[nameof(Resources.This_Role_Not_Found)]), null, null);
            }

            var existingPermissions = await _permissionRepository.FindByAsync(_ => permissionIds.Contains(_.PermissionId));
            if (existingPermissions.Count != permissionIds.Count)
            {
                return (BaseResponse.NotFound(_localizer[nameof(Resources.This_Permission_Not_Found)]), null, null);
            }

            var existingRolePermissions = await _rolePermissionRepository.FindByAsync(_ => _.RoleId == request.RoleId);
            if (existingRolePermissions.Count == 0)
            {
                return (BaseResponse.NotFound(_localizer[nameof(Resources.This_RolePermission_Not_Found)]), null, null);
            }

            return new(BaseResponse.Ok(), existingPermissions!, existingRolePermissions!);
        }

        #endregion
    }
}
