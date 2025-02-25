using DotnetSkeleton.Core.Domain.Interfaces.Services;
using DotnetSkeleton.Core.Domain.Models.Requests.Permissions;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using DotnetSkeleton.SharedKernel.Utils.Models.Requests;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DotnetSkeleton.Core.Application.Services
{
    public class PermissionService : IPermissionService
    {
        #region Private Fields
        private readonly ILogger<PermissionService> _logger;
        private readonly SystemInfoOptions _systemInfoOptions;

        #endregion

        #region Contructor
        public PermissionService(ILogger<PermissionService> logger, IOptions<SystemInfoOptions> systemInfoOptions)
        {
            _logger = logger;
            _systemInfoOptions = systemInfoOptions.Value;
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
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Get,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = string.Format(Constant.ApiEndpoints.PermissionEndpoints.GetPermissionByPermissionId, permissionId),
                RequestSource = Constant.ServiceName.CoreService
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Get all permissions
        /// </summary>
        /// <param name="request">The request containing filtering, sorting, and pagination information for retrieving permissions.</param>
        /// <returns>A <see cref="BaseResponse"/> containing the result of the operation, including permission data or an error if the operation fails.</returns>
        public async Task<BaseResponse> GetAllPermissionsAsync(GetAllPermissionsRequest request)
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

            if (!string.IsNullOrEmpty(request.RoleName))
            {
                queryParams[Constant.ParamAttribute.RoleName] = request.RoleName;
            }

            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Get,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = Constant.ApiEndpoints.PermissionEndpoints.GetAllPermissionsWithPagination,
                RequestSource = Constant.ServiceName.CoreService,
                QueryParams = queryParams
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Create a new permission with the provided information.
        /// </summary>
        /// <param name="request">The details of the permission to create.</param>
        /// <returns>An action result representing the result of the permission created process.</returns>
        public async Task<BaseResponse> CreatePermissionAsync(CreatePermissionRequest request)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Post,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = Constant.ApiEndpoints.PermissionEndpoints.CreatePermission,
                RequestSource = Constant.ServiceName.CoreService,
                Body = request
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Update a permission with the provided information.
        /// </summary>
        /// <param name="request">The details of the permission to update.</param>
        /// <returns>An action result representing the result of the permission updated process.</returns>
        public async Task<BaseResponse> UpdatedPermissionAsync(UpdatePermissionRequest request)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Put,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = Constant.ApiEndpoints.PermissionEndpoints.UpdatePermission,
                RequestSource = Constant.ServiceName.CoreService,
                Body = request
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Delete a existing permission (sort delete)
        /// </summary>
        /// <param name="permissionId">An id of permission</param>
        /// <returns>An action result representing the result of the permission deleted process.</returns>
        public async Task<BaseResponse> DeletePermissionAsync(int permissionId)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Delete,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = string.Format(Constant.ApiEndpoints.PermissionEndpoints.DeletePermission, permissionId),
                RequestSource = Constant.ServiceName.CoreService
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }
        #endregion
    }
}