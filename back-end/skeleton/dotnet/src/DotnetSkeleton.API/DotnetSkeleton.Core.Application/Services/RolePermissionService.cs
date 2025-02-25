using DotnetSkeleton.Core.Domain.Interfaces.Services;
using DotnetSkeleton.Core.Domain.Models.Requests.RolePermissions;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using DotnetSkeleton.SharedKernel.Utils.Models.Requests;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DotnetSkeleton.Core.Application.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        #region Private Fields
        private readonly ILogger<RolePermissionService> _logger;
        private readonly SystemInfoOptions _systemInfoOptions;

        #endregion

        #region Contructor
        public RolePermissionService(ILogger<RolePermissionService> logger, IOptions<SystemInfoOptions> systemInfoOptions)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _systemInfoOptions = systemInfoOptions.Value;
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
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Post,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = Constant.ApiEndpoints.RolePermissionEndpoints.CreateRolePermission,
                RequestSource = Constant.ServiceName.CoreService,
                Body = request
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Update a rolePermission with the provided information.
        /// </summary>
        /// <param name="request">The details of the rolePermission to update.</param>
        /// <returns>An action result representing the result of the rolePermission updated process.</returns>
        public async Task<BaseResponse> UpdatedRolePermissionAsync(UpdateRolePermissionRequest request)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Put,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = Constant.ApiEndpoints.RolePermissionEndpoints.UpdateRolePermission,
                RequestSource = Constant.ServiceName.CoreService,
                Body = request
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Delete a existing rolePermission (sort delete)
        /// </summary>
        /// <param name="rolePermissionId">An id of rolePermission</param>
        /// <returns>An action result representing the result of the rolePermission deleted process.</returns>
        public async Task<BaseResponse> DeleteRolePermissionAsync(int rolePermissionId)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Delete,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = string.Format(Constant.ApiEndpoints.RolePermissionEndpoints.DeleteRolePermission, rolePermissionId),
                RequestSource = Constant.ServiceName.CoreService,
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        #endregion
    }
}