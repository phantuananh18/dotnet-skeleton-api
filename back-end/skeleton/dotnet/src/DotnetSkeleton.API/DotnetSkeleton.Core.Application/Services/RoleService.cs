using DotnetSkeleton.Core.Domain.Interfaces.Services;
using DotnetSkeleton.Core.Domain.Models.Requests.RolePermissions;
using DotnetSkeleton.Core.Domain.Models.Requests.Roles;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using DotnetSkeleton.SharedKernel.Utils.Models.Requests;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DotnetSkeleton.Core.Application.Services
{
    public class RoleService : IRoleService
    {
        #region Private Fields
        private readonly ILogger<RoleService> _logger;
        private readonly SystemInfoOptions _systemInfoOptions;

        #endregion

        #region Contructor
        public RoleService(ILogger<RoleService> logger, IOptions<SystemInfoOptions> systemInfoOptions)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _systemInfoOptions = systemInfoOptions.Value;
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
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Get,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = string.Format(Constant.ApiEndpoints.RoleEndpoints.GetRoleByRoleId, roleId),
                RequestSource = Constant.ServiceName.CoreService,
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Retrieves all roles with pagination based on the specified request parameters.
        /// </summary>
        /// <param name="request">The request containing searching, sorting, and pagination information for retrieving roles.</param>
        /// <returns>A <see cref="BaseResponse"/> containing the result of the operation, including role data or an error if the operation fails.</returns>
        public async Task<BaseResponse> GetAllRolesWithPaginationAsync(GetAllRolesRequest request)
        {
            var queryParams = new Dictionary<string, string>
            {
                [Constant.ParamAttribute.PageNumber] = request.PageNumber.ToString(),
                [Constant.ParamAttribute.PageSize] = request.PageSize.ToString()
            };

            if (request.Filter != null && request.Filter.Any())
            {
                queryParams[Constant.ParamAttribute.Filter] = string.Join(Constant.QueryPrefix.Filter, request.Filter);
            }

            if (!string.IsNullOrEmpty(request.FilterCondition))
            {
                queryParams[Constant.ParamAttribute.FilterCondition] = request.FilterCondition;
            }

            if (request.Sort != null && request.Sort.Any())
            {
                queryParams[Constant.ParamAttribute.Sort] = string.Join(Constant.QueryPrefix.Sort, request.Sort);
            }

            if (!string.IsNullOrEmpty(request.SearchText))
            {
                queryParams[Constant.ParamAttribute.SearchText] = request.SearchText;
            }

            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Get,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = Constant.ApiEndpoints.RoleEndpoints.GetAllRolesWithPagination,
                RequestSource = Constant.ServiceName.CoreService,
                QueryParams = queryParams
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Create a new role with the provided information.
        /// </summary>
        /// <param name="request">The details of the role to create.</param>
        /// <returns>An action result representing the result of the role created process.</returns>
        public async Task<BaseResponse> CreateRoleAsync(CreateRoleRequest request)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Post,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = Constant.ApiEndpoints.RoleEndpoints.CreateRole,
                RequestSource = Constant.ServiceName.CoreService,
                Body = request
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Assign the role permissions with the provided information.
        /// </summary>
        /// <param name="roleId">The Role ID</param>
        /// <param name="request">The details of the role permissions to assign.</param>
        /// <returns>An action result representing the result of the role permissions assigned process.</returns>
        public async Task<BaseResponse> AssignRolePermissionsAsync(int roleId, AssignRolePermissionRequest request)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Post,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = string.Format(Constant.ApiEndpoints.RoleEndpoints.AssignRolePermission, request.RoleId),
                RequestSource = Constant.ServiceName.CoreService,
                Body = request
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Update a role with the provided information.
        /// </summary>
        /// <param name="request">The details of the role to update.</param>
        /// <returns>An action result representing the result of the role updated process.</returns>
        public async Task<BaseResponse> UpdatedRoleAsync(UpdateRoleRequest request)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Put,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = Constant.ApiEndpoints.RoleEndpoints.UpdateRole,
                RequestSource = Constant.ServiceName.CoreService,
                Body = request
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Delete a existing role (sort delete)
        /// </summary>
        /// <param name="roleId">An id of role</param>
        /// <returns>An action result representing the result of the role deleted process.</returns>
        public async Task<BaseResponse> DeleteRoleAsync(int roleId)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Delete,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = string.Format(Constant.ApiEndpoints.RoleEndpoints.DeleteRole, roleId),
                RequestSource = Constant.ServiceName.CoreService
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        #endregion
    }
}