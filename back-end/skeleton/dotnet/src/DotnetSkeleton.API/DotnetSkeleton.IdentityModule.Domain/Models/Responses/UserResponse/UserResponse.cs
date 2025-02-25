namespace DotnetSkeleton.IdentityModule.Domain.Models.Responses.UserResponse
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MobilePhone { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public RoleResponse? Role { get; set; }
        public UserAccountResponse? UserAccount { get; set; }
    }

    public class RoleResponse
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
    }

    public class UserAccountResponse
    {
        public int UserAccountId { get; set; }
        public int? UserId { get; set; }
        public string? AuthMethod { get; set; }
        public string? OAuthProvider { get; set; }
        public string? OAuthProviderAccountId { get; set; }
        public string? SSOProvider { get; set; }
        public string? SSOProviderUserId { get; set; }
        public bool TwoFactorEnabled { get; set; } = false;
        public string? TwoFactorSecret { get; set; }
        public string? TwoFactorBackupCodes { get; set; }
    }
}