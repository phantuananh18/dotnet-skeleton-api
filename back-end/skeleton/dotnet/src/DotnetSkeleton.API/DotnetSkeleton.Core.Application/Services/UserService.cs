using DotnetSkeleton.Core.Domain.Interfaces.Services;
using DotnetSkeleton.Core.Domain.Models.Requests.Users;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using DotnetSkeleton.SharedKernel.Utils.Models.Requests;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DotnetSkeleton.Core.Application.Services
{
    public class UserService : IUserService
    {
        #region Private Fields
        private readonly ILogger<UserService> _logger;
        private readonly SystemInfoOptions _systemInfoOptions;

        #endregion

        #region Constructor
        public UserService(ILogger<UserService> logger, IOptions<SystemInfoOptions> systemInfOptions)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _systemInfoOptions = systemInfOptions.Value;
        }

        #endregion

        #region Public Methods
        // TO-DO: Implement public methods

        /// <summary>
        /// Get a user by userId
        /// </summary>
        /// <param name="userId">An id of user</param>
        /// <returns> List of users </returns>
        public async Task<BaseResponse> GetUserByUserIdAsync(int userId)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Get,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = string.Format(Constant.ApiEndpoints.UserEndpoints.GetUserByUserId, userId),
                RequestSource = Constant.ServiceName.CoreService
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Retrieves all users based on the specified request parameters.
        /// </summary>
        /// <param name="request"> <see cref="GetAllUsersRequest"/>The request containing filtering, sorting, and pagination information for retrieving users.</param>
        /// <returns>A <see cref="BaseResponse"/> containing the result of the operation, including user data or an error if the operation fails.</returns>
        public async Task<BaseResponse> GetAllUsersWithPaginationAsync(GetAllUsersRequest request)
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
                Endpoint = Constant.ApiEndpoints.UserEndpoints.GetAllUsersWithPagination,
                RequestSource = Constant.ServiceName.CoreService,
                QueryParams = queryParams
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Create a new user with the provided information.
        /// </summary>
        /// <param name="request">The details of the user to create.</param>
        /// <returns>An action result representing the result of the user created process.</returns>
        public async Task<BaseResponse> CreateUserAsync(CreateUserRequest request)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Post,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = Constant.ApiEndpoints.UserEndpoints.CreateUser,
                RequestSource = Constant.ServiceName.CoreService,
                Body = request
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Update a user with the provided information.
        /// </summary>
        /// <param name="userId">The userId that need to be update</param>
        /// <param name="request">An instance of the <see cref="UpdateUserRequest"/> class containing the details of the user to update.</param>
        /// <returns>An action result representing the result of the user updated process.</returns>
        public async Task<BaseResponse> UpdateUserAsync(int userId, UpdateUserRequest request)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Put,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = string.Format(Constant.ApiEndpoints.UserEndpoints.UpdateUser, userId),
                RequestSource = Constant.ServiceName.CoreService,
                Body = request
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Delete a existing user (sort delete)
        /// </summary>
        /// <param name="userId">An id of user</param>
        /// <returns>An action result representing the result of the user deleted process.</returns>
        public async Task<BaseResponse> DeleteUserAsync(int userId)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Delete,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = string.Format(Constant.ApiEndpoints.UserEndpoints.DeleteUser, userId),
                RequestSource = Constant.ServiceName.CoreService
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        #endregion
    }
}