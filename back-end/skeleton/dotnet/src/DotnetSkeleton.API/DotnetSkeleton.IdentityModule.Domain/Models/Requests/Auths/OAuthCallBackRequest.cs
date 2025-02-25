using DotnetSkeleton.SharedKernel.Utils;

namespace DotnetSkeleton.IdentityModule.Domain.Models.Requests.Auths
{
    public class OAuthCallBackRequest
    {
        public required string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? MobilePhone { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Provider { get; set; }
        public string? ProviderAccountId { get; set; }
        public string? AuthMethod { get; set; } = Constant.AuthMethod.OAuth;
        public string? Role { get; set; } = Constant.RoleType.Client;
    }
}