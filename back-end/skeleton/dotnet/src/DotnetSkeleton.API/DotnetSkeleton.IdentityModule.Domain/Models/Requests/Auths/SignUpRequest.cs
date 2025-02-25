using DotnetSkeleton.SharedKernel.Utils;

namespace DotnetSkeleton.IdentityModule.Domain.Models.Requests.Auths
{
    public class SignUpRequest
    {
        public required string Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MobilePhone { get; set; }
        public string? Role { get; set; } = Constant.RoleType.Client;
    }
}