using System.Text.Json.Serialization;
using DotnetSkeleton.SharedKernel.Utils;

namespace DotnetSkeleton.IdentityModule.Domain.Models.Requests.Users
{
    public class CreateUserRequest
    {
        public required string Email { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? MobilePhone { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public string? Role { get; set; } = Constant.RoleType.Client;

        [JsonIgnore]
        public string? AuthMethod { get; set; } = Constant.AuthMethod.UsernamePassword;
    }
}