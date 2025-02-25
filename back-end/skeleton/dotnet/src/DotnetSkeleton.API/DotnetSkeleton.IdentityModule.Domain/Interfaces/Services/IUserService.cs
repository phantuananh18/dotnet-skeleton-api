using DotnetSkeleton.IdentityModule.Domain.Models.Requests.Users;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;

namespace DotnetSkeleton.IdentityModule.Domain.Interfaces.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Create a new user with the provided information.
        /// </summary>
        /// <param name="request">The details of the user to create.</param>
        /// <returns>An action result representing the result of the user created process.</returns>
        Task<BaseResponse> CreateUser(CreateUserRequest request);
    }
}