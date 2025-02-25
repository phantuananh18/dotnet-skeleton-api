namespace DotnetSkeleton.SharedKernel.Utils.Models.Options;

public class TokenOptions
{
    public static string JsonKey => nameof(TokenOptions);
    public required string JwtSecretKey { get; set; }
    public int JwtExpirationTime { get; set; }
    public required string RefreshTokenSecretKey { get; set; }
    public int RefreshTokenExpirationTime { get; set; }
    public required string ForgotPasswordTokenSecretKey { get; set; }
    public int ForgotPasswordTokenExpirationTime { get; set; }
    public string? TokenIssuer { get; set; }
    public string? TokenAudience { get; set; }
}