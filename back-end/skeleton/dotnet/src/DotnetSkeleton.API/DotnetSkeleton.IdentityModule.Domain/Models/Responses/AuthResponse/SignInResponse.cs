using DotnetSkeleton.IdentityModule.Domain.Models.Responses.UserResponse;

namespace DotnetSkeleton.IdentityModule.Domain.Models.Responses.SignInResponse
{
    public class SignInResponse
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required UserInformation UserInformation { get; set; }
    }
}