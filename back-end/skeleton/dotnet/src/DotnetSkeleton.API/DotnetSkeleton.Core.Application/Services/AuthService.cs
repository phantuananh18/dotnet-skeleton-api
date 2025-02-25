using DotnetSkeleton.Core.Domain.Interfaces.Services;
using DotnetSkeleton.Core.Domain.Models.Requests.Auths;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using DotnetSkeleton.SharedKernel.Utils.Models.Requests;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DotnetSkeleton.Core.Application.Services
{
    public class AuthService : IAuthService
    {
        #region Private Fields
        private readonly ILogger<AuthService> _logger;
        private readonly SystemInfoOptions _systemInfoOptions;

        #endregion

        #region Constructor
        public AuthService(ILogger<AuthService> logger, IOptions<SystemInfoOptions> systemInfOptions)
        {
            _logger = logger;
            _systemInfoOptions = systemInfOptions.Value;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Signs up a new user with the provided information.
        /// </summary>
        /// <param name="signUpRequest"><see cref="SignUpRequest"/> The details of the user to sign up.</param>
        /// <returns><see cref="BaseResponse"/> The result of the user sign-up process.</returns>
        public async Task<BaseResponse> SignUp(SignUpRequest signUpRequest)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Post,
                BaseUrl = _systemInfoOptions.IdentityServiceUrl!,
                Endpoint = Constant.ApiEndpoints.IdentityEndpoints.SignUp,
                RequestSource = Constant.ServiceName.CoreService,
                Body = signUpRequest
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Signs in a user with the provided credentials.
        /// </summary>
        /// <param name="signInRequest"> <see cref="SignInRequest"/> The credentials of the user to sign in.</param>
        /// <returns><see cref="BaseResponse"/> The result of the authentication process, including access and refresh tokens upon successful authentication.</returns>
        public async Task<BaseResponse> SignIn(SignInRequest signInRequest)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Post,
                BaseUrl = _systemInfoOptions.IdentityServiceUrl!,
                Endpoint = Constant.ApiEndpoints.IdentityEndpoints.SignIn,
                RequestSource = Constant.ServiceName.CoreService,
                Body = signInRequest
            };

            var signInResult = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return signInResult ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Generates an access token using the provided refresh token.
        /// </summary>
        /// <param name="refreshToken"><see cref="RefreshTokenRequest"/>>The refresh token used to generate the access token.</param>
        /// <returns> An new the access token generation process.</returns>
        public async Task<BaseResponse> RefreshAccessToken(RefreshTokenRequest refreshToken)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Post,
                BaseUrl = _systemInfoOptions.IdentityServiceUrl!,
                Endpoint = Constant.ApiEndpoints.IdentityEndpoints.RefreshToken,
                RequestSource = Constant.ServiceName.CoreService,
                Body = refreshToken
            };

            var signInResult = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return signInResult ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Initiates the process for resetting a user's password by sending a password reset email to the provided email address.
        /// </summary>
        /// <param name="request">An instance of the <see cref="ForgotPasswordRequest"/> class containing the user's email address.</param>
        /// <returns>An action result indicating the outcome of the password reset request.</returns>
        public async Task<BaseResponse> ForgotPassword(ForgotPasswordRequest request)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Post,
                BaseUrl = _systemInfoOptions.IdentityServiceUrl!,
                Endpoint = Constant.ApiEndpoints.IdentityEndpoints.ForgotPassword,
                RequestSource = Constant.ServiceName.CoreService,
                Body = request
            };

            var signInResult = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return signInResult ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Resets a user's password based on the provided reset password request and token.
        /// </summary>
        /// <param name="token">The token associated with the password reset request.</param>
        /// <param name="request">A string containing the new password for reset.</param>
        /// <returns>An action result indicating the outcome of the password reset operation.</returns>
        public async Task<BaseResponse> ResetPassword(string token, ResetPasswordRequest request)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Post,
                BaseUrl = _systemInfoOptions.IdentityServiceUrl!,
                Endpoint = string.Format(Constant.ApiEndpoints.IdentityEndpoints.ResetPassword, token),
                RequestSource = Constant.ServiceName.CoreService,
                Body = request
            };

            var signInResult = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return signInResult ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// OAuth callback process.
        /// </summary>
        /// <param name="request">The SignUp request used for OAuth call back request</param>
        /// <returns>The base response represent for the task result of callback process</returns>
        public async Task<BaseResponse> OAuthCallBack(OAuthCallBackRequest request)
        {
            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Post,
                BaseUrl = _systemInfoOptions.IdentityServiceUrl!,
                Endpoint = Constant.ApiEndpoints.IdentityEndpoints.OAuthCallBack,
                RequestSource = Constant.ServiceName.CoreService,
                Body = request
            };

            var signInResult = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return signInResult ?? BaseResponse.ServerError();
        }

        #endregion
    }
}