using AutoMapper;
using DotnetSkeleton.IdentityModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.IdentityModule.Domain.Interfaces.Services;
using DotnetSkeleton.IdentityModule.Domain.Models.Dto;
using DotnetSkeleton.IdentityModule.Domain.Models.Responses.SignInResponse;
using DotnetSkeleton.IdentityModule.Domain.Models.Responses.UserResponse;
using DotnetSkeleton.IdentityModule.Domain.Resources;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Json;
using DotnetSkeleton.IdentityModule.Domain.Models.Requests.Auths;
using DotnetSkeleton.IdentityModule.Domain.Models.Requests.Emails;
using DotnetSkeleton.IdentityModule.Domain.Models.Requests.Users;
using DotnetSkeleton.SharedKernel.Utils.Models.Requests;

namespace DotnetSkeleton.IdentityModule.Application.Services
{
    public class AuthService : IAuthService
    {
        #region Private Fields
        // Repositories
        private readonly IUserRepository _userRepository;

        // Services
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        // Others
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;
        private readonly IStringLocalizer<Resources> _localizer;
        private readonly HttpClient _httpClient;

        // Options
        private readonly TokenOptions _tokenOptions;
        private readonly EncryptOptions _encryptOptions;
        private readonly SystemInfoOptions _systemInfoOptions;

        #endregion

        #region Constructor
        public AuthService(IUserRepository userRepository, IMapper mapper, ILogger<AuthService> logger,
            IStringLocalizer<Resources> localizer, IOptions<TokenOptions> tokenOptions, IOptions<EncryptOptions> encryptOptions,
            IOptions<SystemInfoOptions> systemInfoOptions, IUserService userService, HttpClient httpClient,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _localizer = localizer;
            _userService = userService;
            _tokenService = tokenService;
            _tokenOptions = tokenOptions.Value;
            _encryptOptions = encryptOptions.Value;
            _systemInfoOptions = systemInfoOptions.Value;
            _httpClient = httpClient;
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
            _logger.LogInformation($"[SignUp] Starting sign-up process for user: {signUpRequest.Username}");

            // Validate and create user
            var createUserRequest = _mapper.Map<CreateUserRequest>(signUpRequest);
            var addUserResult = await _userService.CreateUser(createUserRequest);
            if (addUserResult.Status != StatusCodes.Status200OK)
            {
                return addUserResult;
            }

            var user = (UserResponse)addUserResult.Data!;
            var (accessToken, refreshToken) = await GenerateAccessAndRefreshTokens(user.UserId.ToString(), user.Role!.RoleName);
            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            {
                _logger.LogError($"[SignUp] Failed to generate access token or refresh token for user ID: {user.UserId}");
                return BaseResponse.ServerError();
            }

            return BaseResponse.Ok(new SignInResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserInformation = _mapper.Map<UserInformation>(user)
            });
        }

        /// <summary>
        /// Signs in a user with the provided credentials.
        /// </summary>
        /// <param name="signInRequest"> <see cref="SignInRequest"/> The credentials of the user to sign in.</param>
        /// <returns><see cref="BaseResponse"/> The result of the authentication process, including access and refresh tokens upon successful authentication.</returns>
        public async Task<BaseResponse> SignIn(SignInRequest signInRequest)
        {
            _logger.LogInformation($"[SignIn] Starting sign-in process for user: {signInRequest.Username}");
            var existingUser = await _userRepository.FindUserAndRelatedDataByCriteria(Constant.FieldName.Username, signInRequest.Username);
            if (existingUser == null)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_User_Not_Found)]);
            }

            if (existingUser.IsDeleted)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_User_Has_Been_Deactivated)]);
            }

            if (!BCrypt.Net.BCrypt.Verify(signInRequest.Password, existingUser.Password))
            {
                return BaseResponse.Unauthorized(Resources.Incorrect_Password);
            }

            // Generate access token and refresh token
            var claims = new List<Claim> { new(Constant.AuthenticateAttribute.Id, existingUser.UserId.ToString()) };
            var refreshToken = await _tokenService.GenerateToken(_tokenOptions.RefreshTokenSecretKey, claims, _tokenOptions.RefreshTokenExpirationTime);

            claims.Add(new Claim(Constant.AuthenticateAttribute.Role, existingUser.Role?.Name ?? ""));
            var accessToken = await _tokenService.GenerateToken(_tokenOptions.JwtSecretKey, claims, _tokenOptions.JwtExpirationTime);
            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            {
                _logger.LogError($"[SignIn] Failed to generate access token or refresh token for user ID: {existingUser.UserId}");
                return BaseResponse.ServerError();
            }

            return BaseResponse.Ok(new SignInResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserInformation = _mapper.Map<UserInformation>(existingUser)
            });
        }

        /// <summary>
        /// Generates an access token using the provided refresh token.
        /// </summary>
        /// <param name="refreshToken"><see cref="RefreshTokenRequest"/>>The refresh token used to generate the access token.</param>
        /// <returns> An new the access token generation process.</returns>
        public async Task<BaseResponse> RefreshAccessToken(RefreshTokenRequest refreshToken)
        {
            _logger.LogInformation("[RefreshAccessToken] Start to generate access token from refresh token.");
            var decoded = await _tokenService.ValidateJwtToken(_tokenOptions.RefreshTokenSecretKey, refreshToken.RefreshToken);
            if (decoded.Identity is { IsAuthenticated: false })
            {
                return BaseResponse.ServerError(Resources.RefreshToken_Is_Not_Authenticated);
            }

            var userId = decoded.Claims.First(_ => _.Type == Constant.AuthenticateAttribute.Id).Value;
            var existingUser = await _userRepository.FindUserAndRelatedDataByCriteria(Constant.FieldName.UserId, userId);
            if (existingUser == null)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_User_Not_Found)]);
            }

            if (existingUser.IsDeleted)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_User_Has_Been_Deactivated)]);
            }

            var claims = new List<Claim>()
            {
                new (Constant.AuthenticateAttribute.Id, existingUser.UserId.ToString()),
                new (Constant.AuthenticateAttribute.Role, existingUser.Role?.Name ?? "")
            };

            var accessToken = await _tokenService.GenerateToken(_tokenOptions.JwtSecretKey, claims, _tokenOptions.JwtExpirationTime);
            if (string.IsNullOrEmpty(accessToken))
            {
                _logger.LogError($"[RefreshAccessToken] Failed to generate access token for user ID: {existingUser.UserId} with role name: {existingUser.Role?.Name}");
                return BaseResponse.ServerError();
            }

            return BaseResponse.Ok(accessToken);
        }

        /// <summary>
        /// Initiates the process for resetting a user's password by sending a password reset email to the provided email address.
        /// </summary>
        /// <param name="request">An instance of the <see cref="ForgotPasswordRequest"/> class containing the user's email address.</param>
        /// <returns>An action result indicating the outcome of the password reset request.</returns>
        public async Task<BaseResponse> ForgotPassword(ForgotPasswordRequest request)
        {
            var user = await _userRepository.FindOneAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_User_Not_Found)]);
            }

            _logger.LogInformation($"[ForgotPassword] Starting forgot password process for UserId = {user.UserId}");
            var token = await GenerateForgotPasswordToken(user.Email);
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogError($"[ForgotPassword] Failed to generate forgot password token for UserId = {user.UserId}");
                return BaseResponse.ServerError();
            }

            var callbackUrl = $"{_systemInfoOptions.ForgotPasswordCallbackUrl}?token={token}";
            var sendMailRequest = await BuildAndSendForgotPasswordEmail(callbackUrl, user.Email);
            if (sendMailRequest.Status != StatusCodes.Status200OK)
            {
                _logger.LogError($"[ForgotPassword] Failed to send forgot password email for UserId = {user.UserId}");
                return BaseResponse.ServerError();
            }

            return BaseResponse.Ok();
        }

        /// <summary>
        /// Resets a user's password based on the provided reset password request and token.
        /// </summary>
        /// <param name="token">The token associated with the password reset request.</param>
        /// <param name="newPassword">A string containing the new password for reset.</param>
        /// <returns>An action result indicating the outcome of the password reset operation.</returns>
        public async Task<BaseResponse> ResetPassword(string token, string newPassword)
        {
            // Validate reset password token
            var decoded = await _tokenService.ValidateJwtToken(_tokenOptions.ForgotPasswordTokenSecretKey, token);
            if (decoded.Identity is { IsAuthenticated: false })
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.Invalid_Token)]);
            }

            // Claim email from token and find user by email
            var userEmail = decoded.Claims.First(c => c.Type.ToLower().Contains(Constant.FieldName.Email.ToLower())).Value;
            var existingUser = await _userRepository.FindOneAsync(u => u.Email == userEmail && u.IsDeleted == false);
            if (existingUser == null)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_User_Not_Found)]);
            }

            _logger.LogInformation($"[ResetPassword] Starting reset password process for UserId = {existingUser.UserId}");

            // Hash and update password
            existingUser.Password = BCrypt.Net.BCrypt.HashPassword(newPassword, _encryptOptions.BcryptSaltRound);
            var result = await _userRepository.UpdateAsync(existingUser);

            // Send an email to user to notify that password has been reset successfully
            await BuildAndSendResetPasswordSuccessfullyEmail(existingUser.Email);

            return result.UserId > 0 ? BaseResponse.Ok() : BaseResponse.ServerError();
        }

        /// <summary>
        /// OAuth callback process.
        /// </summary>
        /// <param name="request">The SignUp request used for OAuth call back request</param>
        /// <returns>The base response represent for the task result of callback process</returns>
        public async Task<BaseResponse> OAuthCallBack(OAuthCallBackRequest request)
        {
            _logger.LogInformation("[OAuthCallBack] Start callback process for ProviderUserId = {authProviderUserId}", request.ProviderAccountId);
            var userInfo = await _userRepository.GetUserInfoByEmail(request.Email);
            UserInformation response;

            // Case 1. If user is not exist, create a new one and related info
            if (userInfo?.User == null)
            {
                var createUserResult = await HandleNewOAuthUser(request);
                if (createUserResult is not { Status: StatusCodes.Status200OK })
                {
                    return createUserResult;
                }

                response = JsonSerializer.Deserialize<UserInformation>(JsonSerializer.Serialize(createUserResult.Data))!;
            }
            // Case 2. If user exists, add or update UserAccount record for the existing user account based on the OAuthProvider
            else
            {
                var updateUserResult = await HandleExistingOAuthUser(request, userInfo);
                if (updateUserResult is not { Status: StatusCodes.Status200OK })
                {
                    return updateUserResult;
                }

                response = JsonSerializer.Deserialize<UserInformation>(JsonSerializer.Serialize(updateUserResult.Data))!;
            }

            // Generate token for this OAuth user for the logging.
            var (accessToken, refreshToken) = await GenerateAccessAndRefreshTokens(response.UserId.ToString(), response.Role);
            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            {
                _logger.LogError("[SignUp] Failed to generate access token or refresh token for user ID: {userId}",
                    response.UserId.ToString());
                return BaseResponse.ServerError();
            }

            return BaseResponse.Ok(new SignInResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserInformation = response
            });
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generates access token and refresh token for user after successfully sign up or sign in.
        /// </summary>
        /// <param name="userId">The Id of the sign up or sign in user</param>
        /// <param name="role">The role name of the sign up or sign in user</param>
        /// <returns>A tuple (string, string) response that contains access token and refresh token</returns>
        private async Task<(string, string)> GenerateAccessAndRefreshTokens(string userId, string? role)
        {
            // Generate access token and refresh token
            var claims = new List<Claim> { new(Constant.AuthenticateAttribute.Id, userId) };
            var refreshToken = await _tokenService.GenerateToken(
                _tokenOptions.RefreshTokenSecretKey,
                claims,
                _tokenOptions.RefreshTokenExpirationTime);

            if (!string.IsNullOrEmpty(role))
            {
                claims.Add(new(Constant.AuthenticateAttribute.Role, role));
            }

            var accessToken = await _tokenService.GenerateToken(
                _tokenOptions.JwtSecretKey,
                claims,
                _tokenOptions.JwtExpirationTime);

            return (accessToken, refreshToken);
        }

        /// <summary>
        /// Generates a unique token for initiating the password reset process.
        /// </summary>
        /// <param name="email">The user's email address associated with the password reset.</param>
        /// <returns>A unique token string for initiating the password reset process.</returns>
        private async Task<string> GenerateForgotPasswordToken(string email)
        {
            var claims = new List<Claim>()
            {
                new (Constant.FieldName.Email.ToLower(), email)
            };
            return await _tokenService.GenerateToken(_tokenOptions.ForgotPasswordTokenSecretKey, claims, _tokenOptions.ForgotPasswordTokenExpirationTime);
        }

        /// <summary>
        /// Build and send forgot password email to user.
        /// </summary>
        /// <param name="callbackUrl">The Callback Url which will be embedded in the mail</param>
        /// <param name="receiver">The user's email</param>
        /// <returns>A boolean value that represent for the result of the send mail task</returns>
        private async Task<BaseResponse> BuildAndSendForgotPasswordEmail(string callbackUrl, string receiver)
        {
            // Prepare data to send email
            var payload = new OutgoingEmailRequest
            {
                EmailType = Constant.EmailType.NoReply,
                To = new List<string> { receiver },
                TemplateName = Constant.EmailTemplateName.ResetPassword,
                TemplatePlaceHolders = new Dictionary<string, string>()
                {
                    { Constant.EmailPlaceHolder.CallbackUrl, callbackUrl }
                }
            };

            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Post,
                BaseUrl = _systemInfoOptions.EmailServiceUrl!,
                Endpoint = Constant.ApiEndpoints.EmailEndpoints.SendMail,
                RequestSource = Constant.ServiceName.IdentityService,
                Body = payload
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();
        }

        /// <summary>
        /// Build and send reset password successfully email to user.
        /// </summary>
        /// <param name="receiver">The user's email</param>
        /// <returns>A boolean value that represent for the result of the send mail task</returns>
        private async Task<BaseResponse> BuildAndSendResetPasswordSuccessfullyEmail(string receiver)
        {
            // Prepare data to send email
            var payload = new OutgoingEmailRequest
            {
                EmailType = Constant.EmailType.NoReply,
                To = new List<string> { receiver },
                TemplateName = Constant.EmailTemplateName.ResetPasswordSuccess,
            };

            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Post,
                BaseUrl = _systemInfoOptions.EmailServiceUrl!,
                Endpoint = Constant.ApiEndpoints.EmailEndpoints.SendMail,
                RequestSource = Constant.ServiceName.IdentityService,
                Body = payload
            };

            var result = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return result ?? BaseResponse.ServerError();

        }

        private async Task<BaseResponse> HandleNewOAuthUser(OAuthCallBackRequest request)
        {
            var payload = _mapper.Map<CreateUserPayload>(request);
            payload.FirstName = ExtractFirstName(request.FirstName, request.FullName);
            payload.LastName = ExtractLastName(request.LastName, request.FullName);
            payload.UserAccount = _mapper.Map<UserAccountRequest>(request);
            _logger.LogInformation("[HandleNewOAuthUser] Start process add new OAuth user for {firstName} {lastName}",
                payload.FirstName, payload.LastName);

            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Post,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = Constant.ApiEndpoints.UserEndpoints.CreateUser,
                RequestSource = Constant.ServiceName.IdentityService,
                Body = payload
            };

            var createUserResult = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            _logger.LogInformation("[HandleNewOAuthUser] Sent Http request to UserService to add new OAuth user");

            return createUserResult ?? BaseResponse.ServerError();
        }

        private async Task<BaseResponse> HandleExistingOAuthUser(OAuthCallBackRequest request, UserInfo userInfo)
        {
            if (userInfo.User == null)
            {
                _logger.LogError("[HandleExistingOAuthUser] User not found");
                return BaseResponse.BadRequest();
            }

            var payload = _mapper.Map<UpdateUserPayload>(request);
            payload.FirstName = ExtractFirstName(request.FirstName, request.FullName);
            payload.LastName = ExtractLastName(request.LastName, request.FullName);
            payload.UserAccount = _mapper.Map<UserAccountRequest>(request);

            _logger.LogInformation("[HandleExistingOAuthUser] Start process update existing OAuth user for {firstName} {lastName}",
                payload.FirstName, payload.LastName);

            var requestParam = new BaseHttpRequestParameter()
            {
                Method = HttpMethod.Put,
                BaseUrl = _systemInfoOptions.UserServiceUrl!,
                Endpoint = string.Format(Constant.ApiEndpoints.UserEndpoints.UpdateUser, userInfo.User.UserId),
                RequestSource = Constant.ServiceName.IdentityService,
                Body = payload
            };

            var updateUserResult = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            _logger.LogInformation("[HandleExistingOAuthUser] Sent Http request to UserService to update new OAuth user");

            return updateUserResult ?? BaseResponse.ServerError();
        }

        private static string? ExtractFirstName(string? firstName, string? fullName)
        {
            if (!string.IsNullOrEmpty(firstName))
            {
                return firstName;
            }

            return !string.IsNullOrEmpty(fullName)
                ? fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries).First()
                : firstName;
        }

        private static string? ExtractLastName(string? lastName, string? fullName)
        {
            if (!string.IsNullOrEmpty(lastName))
            {
                return lastName;
            }

            return !string.IsNullOrEmpty(fullName)
                ? fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries).First()
                : lastName;
        }

        #endregion
    }
}