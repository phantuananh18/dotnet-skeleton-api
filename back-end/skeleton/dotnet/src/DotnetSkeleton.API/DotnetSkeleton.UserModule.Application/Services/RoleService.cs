using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.UserModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using DotnetSkeleton.UserModule.Domain.Model.Responses.RoleResponses;
using DotnetSkeleton.UserModule.Domain.Model.Utils;
using DotnetSkeleton.UserModule.Domain.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using DotnetSkeleton.UserModule.Domain.Model.Requests.Roles;

namespace DotnetSkeleton.UserModule.Application.Services
{
    public class RoleService : IRoleService
    {
        #region Private Fields
        private readonly IRoleRepository _roleRepository;
        private readonly IStringLocalizer<Resources> _localizer;
        private readonly ILogger<RoleService> _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Contructor
        public RoleService(IRoleRepository roleRepository,
            ILogger<RoleService> logger,
            IMapper mapper,
            IStringLocalizer<Resources> localizer)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Get a role by roleId
        /// </summary>
        /// <param name="roleId">An id of role</param>
        /// <returns> List of roles </returns>
        public async Task<BaseResponse> GetRoleByRoleIdAsync(int roleId)
        {
            var role = await _roleRepository.FindByIdAsync(roleId);
            return role != null
                ? BaseResponse.Ok(role)
                : BaseResponse.NotFound();
        }

        /// <summary>
        /// Create a new role with the provided information.
        /// </summary>
        /// <param name="request">The details of the role to create.</param>
        /// <returns>An action result representing the result of the role created process.</returns>
        public async Task<BaseResponse> CreateRoleAsync(CreateRoleRequest request)
        {
            var validateUserResult = await ValidateRoleInfoAsync(request);
            if (validateUserResult.Status != (int)HttpStatusCode.OK)
            {
                return validateUserResult;
            }

            var roleEntity = _mapper.Map<Role>(request);
            var newRole = await _roleRepository.AddAsync(roleEntity);

            return BaseResponse.Ok(_mapper.Map<RoleResponse>(newRole));
        }

        /// <summary>
        /// Update a role with the provided information.
        /// </summary>
        /// <param name="roleId">The Role ID</param>
        /// <param name="request">The details of the role to update.</param>
        /// <returns>An action result representing the result of the role updated process.</returns>
        public async Task<BaseResponse> UpdatedRoleAsync(int roleId, UpdateRoleRequest request)
        {
            var role = await _roleRepository.FindByIdAsync(roleId);
            if (role == null || role.IsDeleted)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_Role_Not_Found)]);
            }

            role.Name = request.Name;
            role.Description = request.Description;
            var updatedRole = await _roleRepository.UpdateAsync(role);

            return BaseResponse.Ok(_mapper.Map<RoleResponse>(updatedRole));
        }

        /// <summary>
        /// Delete a existing role (sort delete)
        /// </summary>
        /// <param name="roleId">An id of role</param>
        /// <returns>An action result representing the result of the role deleted process.</returns>
        public async Task<BaseResponse> DeleteRoleAsync(int roleId)
        {
            var role = await _roleRepository.FindByIdAsync(roleId);
            if (role == null || role.IsDeleted)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_Role_Not_Found)]);
            }

            role.IsDeleted = true;
            var deletedRole = await _roleRepository.UpdateAsync(role);

            return BaseResponse.Ok(new { deletedRole.RoleId });
        }

        public async Task<BaseResponse> GetAllRolesWithPaginationAsync(GetAllRolesRequest request)
        {
            _logger.LogInformation($"[GetAllRolesWithPaginationAsync] - Start to get all roles with pagination. Request: {JsonSerializer.Serialize(request)}");

            string searchText = string.Empty;
            if (!string.IsNullOrEmpty(request.SearchText))
            {
                searchText = string.Format(Constant.RoleSearchKeyword.SearchRole, request.SearchText);
            }

            // Refer ticket DWF-99
            // Only one column can be sorted at a time
            // Default sort by role name ascending
            var roleSortColumn = new RoleSortColumnMapping();
            string sort = string.Empty;
            if (request.Sort != null && request.Sort.Length == 1)
            {
                sort = Helpers.SortStringGenerator(request.Sort, roleSortColumn);
            }
            else
            {
                sort = $" {roleSortColumn.Role} ";
            }

            var roles = await _roleRepository.GetAllRolesWithPaginationAsync(request.PageNumber, request.PageSize, searchText, sort);

            int totalRecords = roles.Count > 0 ? roles.FirstOrDefault()!.TotalRecords : 0;
            int totalPages = (int)Math.Ceiling((double)totalRecords / request.PageSize);

            return BaseResponse.Ok(new PaginationBaseResult<List<RolePaginationResponse>>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalNumberOfRecords = totalRecords,
                Results = _mapper.Map<List<RolePaginationResponse>>(roles),
                TotalPages = totalPages
            });
        }

        #endregion

        #region Private Methods

        private async Task<BaseResponse> ValidateRoleInfoAsync(CreateRoleRequest request)
        {
            var existingRole = await _roleRepository
                .FindOneAsync(x => x.Name == request.Name);

            if (existingRole != null)
            {
                if (existingRole.IsDeleted)
                {
                    return BaseResponse.BadRequest(_localizer[nameof(Resources.This_Role_Has_Been_Deactivated)]);
                }

                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_Role_Already_Exists)]);
            }

            return BaseResponse.Ok();
        }

        #endregion
    }
}
