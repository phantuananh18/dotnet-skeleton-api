using AutoMapper;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Entities.MySQLEntities;
using DotnetSkeleton.UserModule.Domain.Interfaces.Repositories;
using DotnetSkeleton.UserModule.Domain.Interfaces.Services;
using DotnetSkeleton.UserModule.Domain.Model.Responses.UserResponses;
using DotnetSkeleton.UserModule.Domain.Model.Utils;
using DotnetSkeleton.UserModule.Domain.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using System.Text.Json;
//using DotnetSkeleton.Utils.RedisService.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using DotnetSkeleton.SharedKernel.Utils.Models.Requests;
using DotnetSkeleton.UserModule.Domain.Model.Requests.Email;
using DotnetSkeleton.UserModule.Domain.Model.Requests.Users;

namespace DotnetSkeleton.UserModule.Application.Services
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
        private readonly SystemInfoOptions _systemInfoOptions;

        // Others
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IStringLocalizer<Resources> _localizer;
        //private readonly IRedisService _redisService;

        #endregion

        #region Constructor
        public UserService(IUserRepository userRepository,
            IMapper mapper,
            ILogger<UserService> logger,
            IRoleRepository roleRepository,
            IUserAccountRepository userAccountRepository,
            IOptions<EncryptOptions> encryptOptions,
            IOptions<SystemInfoOptions> systemInfoOptions,
            IStringLocalizer<Resources> localizer/*, IRedisService redisService*/)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _userAccountRepository = userAccountRepository ?? throw new ArgumentNullException(nameof(userAccountRepository));
            _encryptOptions = encryptOptions.Value;
            _systemInfoOptions = systemInfoOptions.Value;
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
            //_redisService = redisService;
        }

        #endregion

        #region Public Methods
        // TO-DO: Implement public methods

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns> List of users </returns>
        public async Task<BaseResponse> GetAllUsersWithMySQLAsync()
        {
            var users = await _userRepository.FindAllAsync();
            return users.Any()
                ? BaseResponse.Ok(users)
                : BaseResponse.ServerError();
        }

        /// <summary>
        /// Get a user by userId
        /// </summary>
        /// <param name="userId">An id of user</param>
        /// <returns> List of users </returns>
        public async Task<BaseResponse> GetUserByUserIdAsync(int userId)
        {
            /*var cachingResult = await _redisService.GetAsync($"get-user-detail:{userId}");
            if (!string.IsNullOrEmpty(cachingResult))
            {
                return BaseResponse.Ok(JsonSerializer.Deserialize<User>(cachingResult));
            }

            _logger.LogInformation("Not caching");
            var user = await _userRepository.FindByIdAsync(userId);
            if (user == null)
            {
                return BaseResponse.NotFound();
            }

            await _redisService.SetAsync($"get-user-detail:{userId}", JsonSerializer.Serialize(user), new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            });

            return BaseResponse.Ok(user);*/

            var user = await _userRepository.FindByIdAsync(userId);
            return user != null ? BaseResponse.Ok(user) : BaseResponse.ServerError();

        }

        /// <summary>
        /// Retrieves all users based on the specified request parameters.
        /// </summary>
        /// <param name="request"> <see cref="GetAllUsersRequest"/>The request containing filtering, sorting, and pagination information for retrieving users.</param>
        /// <returns>A <see cref="BaseResponse"/> containing the result of the operation, including user data or an error if the operation fails.</returns>
        public async Task<BaseResponse> GetAllUsersWithPaginationAsync(GetAllUsersRequest request)
        {
            _logger.LogInformation($"[GetAllUsersWithPaginationAsync] Start to get all users with pagination. Request: {JsonSerializer.Serialize(request)}");

            var filterBuilder = new StringBuilder();

            if (request.Filter != null && request.Filter.Any())
            {
                filterBuilder.Append(GenerateUserFilterString(request.Filter));
            }

            if (!string.IsNullOrEmpty(request.SearchText))
            {
                filterBuilder.Append(string.Format(Constant.UserSearchKeyword.SearchUser, request.SearchText));
            }

            string filter = filterBuilder.ToString();

            // Refer ticket DWF-98
            // Only one column can be sorted at a time
            // Default sort by first name ascending
            string sort = string.Empty;
            var userSortColumn = new UserSortColumnMapping();
            if (request.Sort != null && request.Sort.Length == 1)
            {
                sort = Helpers.SortStringGenerator(request.Sort, userSortColumn);
            }
            else
            {
                sort = $"{userSortColumn.FirstName}";
            }

            var users = await _userRepository.GetAllUsersWithPaginationAsync(request.PageNumber, request.PageSize, filter, sort);

            int totalRecords = users.Count > 0 ? users.FirstOrDefault()!.TotalRecords : 0;
            int totalPages = (int)Math.Ceiling((double)totalRecords / request.PageSize);

            return BaseResponse.Ok(new PaginationBaseResult<List<UserPaginationResponse>>
            {
                Results = _mapper.Map<List<UserPaginationResponse>>(users),
                TotalNumberOfRecords = totalRecords,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = totalPages
            });
        }

        /// <summary>
        /// Create a new user with the provided information.
        /// </summary>
        /// <param name="request">The details of the user to create.</param>
        /// <returns>An action result representing the result of the user created process.</returns>
        public async Task<BaseResponse> CreateUserAsync(CreateUserRequest request)
        {
            // Step 1. Validate request
            var (validateUserResult, existingRole) = await ValidateUserInfoAsync(request);
            if (validateUserResult.Status != (int)HttpStatusCode.OK)
            {
                return validateUserResult;
            }

            if (existingRole == null)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.Invalid_Role)]);
            }

            // Step 2. Handle required attribute for User
            var userEntity = _mapper.Map<User>(request);

            // Generate username and password in case of signing up using OAuth method
            userEntity.Username = string.IsNullOrEmpty(userEntity.Username)
                ? await GenerateUsernameAsync(request.Email)
                : request.Username;

            var tempPassword = string.IsNullOrEmpty(userEntity.Password)
                ? Helpers.GeneratePassword(8)
                : userEntity.Password;

            userEntity.Password = string.IsNullOrEmpty(userEntity.Password)
                ? BCrypt.Net.BCrypt.HashPassword(tempPassword, _encryptOptions.BcryptSaltRound)
                : BCrypt.Net.BCrypt.HashPassword(request.Password, _encryptOptions.BcryptSaltRound);

            userEntity.RoleId = existingRole.RoleId;
            var addUserResult = await AddOrUpdateUser(userEntity, false);
            if (addUserResult == null)
            {
                return BaseResponse.ServerError();
            }

            // Step 3. Handle add UserAccount record
            var userAccountEntity = new UserAccount()
            {
                UserId = addUserResult.UserId
            };

            if (request.UserAccount == null)
            {
                userAccountEntity.AuthMethod = Constant.AuthMethod.UsernamePassword;
            }
            else
            {
                userAccountEntity = _mapper.Map<UserAccount>(request.UserAccount);
                userAccountEntity.UserId = addUserResult.UserId;
            }
            
            var addUserAccountResult = await AddOrUpdateUserAccount(userAccountEntity, false);
            if (addUserAccountResult == null)
            {
                return BaseResponse.ServerError();
            }

            // Send create account success email
            await BuildAndSendCreateAccountSuccessEmail(userEntity.Email, $"{userEntity.FirstName} {userEntity.LastName}", userEntity.Username!, tempPassword);

            return BaseResponse.Ok(new UserInformation()
            {
                Email = userEntity.Email,
                LastName = userEntity.LastName,
                FirstName = userEntity.FirstName,
                Role = existingRole.Name,
                MobilePhone = userEntity.MobilePhone,
                ProfilePictureUrl = userEntity.ProfilePictureUrl,
                UserId = userEntity.UserId
            });
        }

        /// <summary>
        /// Update a user with the provided information.
        /// </summary>
        /// <param name="userId">The userId that need to be update</param>
        /// <param name="request">An instance of the <see cref="UpdateUserRequest"/> class containing the details of the user to update.</param>
        /// <returns>An action result representing the result of the user updated process.</returns>
        public async Task<BaseResponse> UpdateUserAsync(int userId, UpdateUserRequest request)
        {
            // Step 1. Retrieve user and related information
            var user = await _userRepository.FindUserInfoByIdAsync(userId);
            if (user == null || user.User.IsDeleted)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_User_Not_Found)]);
            }

            // Step 2. Update user role if needed
            if (!string.IsNullOrEmpty(request.Role) && user.Role.Name != request.Role)
            {
                var existingRole = await _roleRepository.FindOneAsync(r => r.Name == request.Role);
                if (existingRole == null || existingRole.IsDeleted)
                {
                    return BaseResponse.BadRequest(_localizer[nameof(Resources.Invalid_Role)]);
                }
                user.User.RoleId = existingRole.RoleId;
            }

            // Step 3. Update basic user information
            user.User.FirstName = request.FirstName ?? user.User.FirstName;
            user.User.LastName = request.LastName ?? user.User.LastName;
            user.User.MobilePhone = request.MobilePhone ?? user.User.MobilePhone;

            var updateUserResult = await AddOrUpdateUser(user.User, true);
            if (updateUserResult == null)
            {
                return BaseResponse.BadRequest("Failed to update user");
            }

            // Step 4. Handle user account update
            if (request.UserAccount != null)
            {
                var userAccountRequest = _mapper.Map<UserAccount>(request.UserAccount);
                userAccountRequest.UserId = updateUserResult.UserId;

                var currentUserAccount = user.UserAccount!.FirstOrDefault(a => a.AuthMethod == request.UserAccount.AuthMethod);
                if (currentUserAccount != null)
                {
                    currentUserAccount.OAuthProvider = userAccountRequest.OAuthProvider;
                    currentUserAccount.OAuthProviderAccountId = userAccountRequest.OAuthProviderAccountId;
                    currentUserAccount.SSOProvider = userAccountRequest.SSOProvider;
                    currentUserAccount.SSOProviderUserId = userAccountRequest.SSOProviderUserId;
                    currentUserAccount.TwoFactorBackupCodes = userAccountRequest.TwoFactorBackupCodes;
                    currentUserAccount.TwoFactorSecret = userAccountRequest.TwoFactorSecret;
                }

                var userAccountResult = await AddOrUpdateUserAccount(
                    currentUserAccount ?? userAccountRequest,
                    currentUserAccount != null);

                if (userAccountResult == null)
                {
                    return BaseResponse.BadRequest("Failed to update user account");
                }
            }
            
            return BaseResponse.Ok(new UserInformation()
            {
                Email = user.User.Email,
                LastName = request.LastName,
                FirstName = request.FirstName,
                Role = request.Role,
                MobilePhone = request.MobilePhone,
                ProfilePictureUrl = request.ProfilePictureUrl,
                UserId = user.User.UserId
            });
        }
        
        /// <summary>
        /// Delete a existing user (sort delete)
        /// </summary>
        /// <param name="userId">An id of user</param>
        /// <returns>An action result representing the result of the user deleted process.</returns>
        public async Task<BaseResponse> DeleteUserAsync(int userId)
        {
            var user = await _userRepository.FindByIdAsync(userId);
            if (user == null || user.IsDeleted)
            {
                return BaseResponse.BadRequest(_localizer[nameof(Resources.This_User_Not_Found)]);
            }

            user.IsDeleted = true;

            var deletedUser = await _userRepository.UpdateAsync(user);
            return BaseResponse.Ok(new { deletedUser.UserId });
        }

        #endregion

        #region Private Methods

        private async Task<UserResponse?> AddOrUpdateUser(User user, bool isUpdate)
        {
            User result;
            if (!isUpdate)
            {
                result = await _userRepository.AddAsync(user);
            }
            else
            {
                result = await _userRepository.UpdateAsync(user);
            }

            // Check if result is valid and return mapped response
            if (result.UserId >= 1)
            {
                return _mapper.Map<UserResponse>(result);
            }

            _logger.LogError("[AddOrUpdateUser] Failed to add or update User in the database");
            return null;
        }
        
        private async Task<UserAccountResponse?> AddOrUpdateUserAccount(UserAccount userAccount, bool isUpdate)
        {
            UserAccount result;
            if (!isUpdate)
            {
                result = await _userAccountRepository.AddAsync(userAccount);
            }
            else
            {
                result = await _userAccountRepository.UpdateAsync(userAccount);
            }

            // Check if result is valid and return mapped response
            if (result.UserAccountId >= 1)
            {
                return _mapper.Map<UserAccountResponse>(result);
            }

            _logger.LogError("[AddOrUpdateUserAccount] Failed to add or update UserAccount in the database");
            return null;
        }

        private async Task<(BaseResponse, Role?)> ValidateUserInfoAsync(CreateUserRequest request)
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

            return (BaseResponse.Ok(), existingRole);
        }

        /// <summary>
        /// Automatically generates a username based on the provided email address.
        /// </summary>
        /// <param name="email"> The email address of user </param>
        /// <returns>A string that represent a generated username of provided email</returns>
        private async Task<string> GenerateUsernameAsync(string email)
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

        private string GenerateUserFilterString(string[] filters)
        {
            var advancedFilter = new List<string>();
            var columnMapping = new UserFilterColumnMapping();
            foreach (var filterItem in filters)
            {
                var filterSpliting = filterItem.Split(',');
                string columnName = Helpers.GetPropValue<string>(columnMapping, filterSpliting[0])!;
                string valueType = filterSpliting[1];
                string filterOperator = filterSpliting[2];
                string value = filterSpliting[3];

                string filterStrings = Helpers.FilterStringGeneration(columnName, valueType, filterOperator, value);
                advancedFilter.Add(filterStrings);
            }

            return $"{Constant.FilterQueries.And} {string.Join(Constant.FilterQueries.And, advancedFilter)}";
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
                RequestSource = Constant.ServiceName.EmailService,
                Body = payload
            };

            var signInResult = await HttpClientHelper.SendHttpRequestAsync<BaseResponse>(requestParam, _logger);
            return signInResult ?? BaseResponse.ServerError();
        }

        #endregion
    }
}
