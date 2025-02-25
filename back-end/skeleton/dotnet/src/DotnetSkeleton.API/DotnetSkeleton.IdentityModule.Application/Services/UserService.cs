using AutoMapper;
using DotnetSkeleton.IdentityModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.IdentityModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.IdentityModule.Domain.Interfaces.Services;
using DotnetSkeleton.IdentityModule.Domain.Models.Requests.Emails;
using DotnetSkeleton.IdentityModule.Domain.Models.Requests.Users;
using DotnetSkeleton.IdentityModule.Domain.Models.Responses.UserResponse;
using DotnetSkeleton.IdentityModule.Domain.Resources;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DotnetSkeleton.SharedKernel.Utils.Models.Requests;

namespace DotnetSkeleton.IdentityModule.Application.Services
{
    public class UserService : IUserService
    {
        #region Private Fields
        // Repositories
        private readonly IUserRepository _userRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IRoleRepository _roleRepository;

        // Options
        private readonly EncryptOptions _encryptOptions;
        private readonly MailOptions _mailOptions;
        private readonly SystemInfoOptions _systemInfoOptions;

        // Others
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<Resources> _localizer;
        private readonly HttpClient _httpClient;

        #endregion

        #region Constructor
        public UserService(IUserRepository userRepository,
            IMapper mapper,
            ILogger<UserService> logger,
            IHttpContextAccessor httpContextAccessor,
            IRoleRepository roleRepository,
            IUserAccountRepository userAccountRepository,
            IOptions<EncryptOptions> encryptOptions,
            IOptions<MailOptions> mailOptions,
            IOptions<SystemInfoOptions> systemInfoOptions,
            IStringLocalizer<Resources> localizer,
            HttpClient httpClient)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _roleRepository = roleRepository;
            _userAccountRepository = userAccountRepository;
            _encryptOptions = encryptOptions.Value;
            _mailOptions = mailOptions.Value;
            _systemInfoOptions = systemInfoOptions.Value;
            _localizer = localizer;
            _httpClient = httpClient;
        }

        #endregion

        #region Public Methods
        // TO-DO: Implement public methods

        /// <summary>
        /// Create a new user with the provided information.
        /// </summary>
        /// <param name="request">The details of the user to create.</param>
        /// <returns>An action result representing the result of the user created process.</returns>
        public async Task<BaseResponse> CreateUser(CreateUserRequest request)
        {
            var (validationResult, existingRole) = await ValidateUserInfo(request);
            if (validationResult.Status != StatusCodes.Status200OK)
            {
                return validationResult;
            }

            // Create User entity
            var userEntity = _mapper.Map<User>(request);

            // Generate username and password in case of signing up using OAuth method
            userEntity.Username = string.IsNullOrEmpty(userEntity.Username) ? await GenerateUsername(request.Email) : request.Username;
            var tempPassword = string.IsNullOrEmpty(userEntity.Password) ? Helpers.GeneratePassword(8) : userEntity.Password;
            userEntity.Password = string.IsNullOrEmpty(userEntity.Password)
                ? BCrypt.Net.BCrypt.HashPassword(tempPassword, _encryptOptions.BcryptSaltRound)
                : BCrypt.Net.BCrypt.HashPassword(request.Password, _encryptOptions.BcryptSaltRound);

            userEntity.RoleId = existingRole!.RoleId;

            // Create UserAccount entity
            var userAccountEntity = new UserAccount
            {
                AuthMethod = request.AuthMethod ?? Constant.AuthMethod.UsernamePassword
            };

            var newUser = await AddUserAndRelatedData(userEntity, userAccountEntity);
            if (newUser == null)
            {
                return BaseResponse.ServerError();
            }

            // Map response
            newUser.Role = new RoleResponse
            {
                RoleId = existingRole.RoleId,
                RoleName = existingRole.Name
            };

            // Send create account success email
            await BuildAndSendCreateAccountSuccessEmail(newUser.Email, $"{newUser.FirstName} {newUser.LastName}", newUser.Username, tempPassword);

            return BaseResponse.Ok(newUser);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Validates the user information provided in the CreateUserRequest.
        /// </summary>
        /// <param name="request">The CreateUserRequest containing the user information.</param>
        /// <returns>A tuple containing a BaseResponse and a Role object if the user information is valid.</returns>
        private async Task<(BaseResponse, Role?)> ValidateUserInfo(CreateUserRequest request)
        {
            if (string.IsNullOrEmpty(request.Role))
            {
                return (BaseResponse.BadRequest(_localizer[nameof(Resources.Invalid_Role)]), null);
            }

            var existingUser = await _userRepository.FindOneAsync(_ =>
                (!string.IsNullOrEmpty(request.Email) && _.Email == request.Email)
                || (!string.IsNullOrEmpty(request.MobilePhone) && _.MobilePhone == request.MobilePhone)
                || (!string.IsNullOrEmpty(request.Username) && _.Username == request.Username));

            if (existingUser != null)
            {
                if (existingUser.IsDeleted)
                {
                    return (BaseResponse.BadRequest(_localizer[nameof(Resources.This_User_Has_Been_Deactivated)]), null);
                }

                return (BaseResponse.BadRequest(_localizer[nameof(Resources.This_User_Already_Exists)]), null);
            }

            var existingRole = await _roleRepository.FindOneAsync(r => r.Name == request.Role && !r.IsDeleted);
            return existingRole == null
                ? (BaseResponse.BadRequest(_localizer[nameof(Resources.Invalid_Role)]), null)
                : (BaseResponse.Ok(), existingRole);
        }

        /// <summary>
        /// Automatically generates a username based on the provided email address.
        /// </summary>
        /// <param name="email"> The email address of user </param>
        /// <returns>A string that represent a generated username of provided email</returns>
        private async Task<string> GenerateUsername(string email)
        {
            var username = email.Split('@').First();
            // Check if any user have the same username
            var existingUser = await _userRepository.FindOneAsync(u => u.Username == username);
            if (existingUser == null)
            {
                return username;
            }

            var suffix = Helpers.Shuffle(Helpers.RandomLetters(3) + Helpers.RandomNumbers(3));
            return $"{username}{suffix}";
        }


        /// <summary>
        /// Adds a new user and related data to the database.
        /// </summary>
        /// <param name="user">The user entity to add.</param>
        /// <param name="userAccount">The user account entity to add.</param>
        /// <returns>A UserResponse object representing the added user and related data, or null if the operation fails.</returns>
        private async Task<UserResponse?> AddUserAndRelatedData(User user, UserAccount userAccount)
        {
            // Add user
            var newUser = await _userRepository.AddAsync(user);
            if (newUser.UserId < 1)
            {
                _logger.LogError("[AddUserAndRelatedData] Failed to add user to database");
                return null;
            }

            var result = _mapper.Map<UserResponse>(newUser);

            // Add user account
            userAccount.UserId = newUser.UserId;
            var newUserAccount = await _userAccountRepository.AddAsync(userAccount);
            if (newUserAccount.UserAccountId < 1)
            {
                _logger.LogError("[AddUserAndRelatedData] Failed to add user account to database");
            }

            result.UserAccount = _mapper.Map<UserAccountResponse>(newUserAccount);
            return result;
        }

        /// <summary>
        /// Build and send create account success email
        /// </summary>
        /// <param name="receiver">The email's receiver</param>
        /// <param name="receiverName">The receiver name</param>
        /// <param name="username">The username of the account</param>
        /// <param name="password">The password/temporary password of the account</param>
        /// <returns>A boolean value that represent for the result of send mail task</returns>
        private async Task<BaseResponse> BuildAndSendCreateAccountSuccessEmail(string receiver, string receiverName, string username, string password)
        {
            var payload = new OutgoingEmailRequest
            {
                EmailType = Constant.EmailType.NoReply,
                To = new List<string> { receiver },
                TemplateName = Constant.EmailTemplateName.WelcomeEmail,
                TemplatePlaceHolders = new Dictionary<string, string>()
                {
                    { Constant.EmailPlaceHolder.UserFullName, receiverName },
                    { Constant.EmailPlaceHolder.UserName, username },
                    { Constant.EmailPlaceHolder.TempPassword, password },
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

            var signInResult = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return signInResult ?? BaseResponse.ServerError();
        }

        #endregion
    }
}