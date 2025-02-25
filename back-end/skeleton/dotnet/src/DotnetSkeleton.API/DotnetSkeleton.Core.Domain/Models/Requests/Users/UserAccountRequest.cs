using DotnetSkeleton.SharedKernel.Utils;

namespace DotnetSkeleton.Core.Domain.Models.Requests.Users;

public class UserAccountRequest
{
    public string? AuthMethod { get; set; } = Constant.AuthMethod.UsernamePassword;

    public string? OAuthProvider { get; set; }

    public string? OAuthProviderAccountId { get; set; }

    public string? SSOProvider { get; set; }

    public string? SSOProviderUserId { get; set; }

    public bool TwoFactorEnabled { get; set; } = false;

    public string? TwoFactorSecret { get; set; }

    public string? TwoFactorBackupCodes { get; set; }
}