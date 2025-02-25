namespace DotnetSkeleton.IdentityModule.Domain.Models.Requests.Users;

public class UserAccountRequest
{
    public string? AuthMethod { get; set; }

    public string? OAuthProvider { get; set; }

    public string? OAuthProviderAccountId { get; set; }

    public string? SSOProvider { get; set; }

    public string? SSOProviderUserId { get; set; }

    public bool TwoFactorEnabled { get; set; } = false;

    public string? TwoFactorSecret { get; set; }

    public string? TwoFactorBackupCodes { get; set; }
}