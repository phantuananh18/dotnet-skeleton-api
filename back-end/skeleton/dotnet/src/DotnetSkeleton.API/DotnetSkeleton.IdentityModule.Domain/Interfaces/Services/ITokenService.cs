using System.Security.Claims;

namespace DotnetSkeleton.IdentityModule.Domain.Interfaces.Services;

public interface ITokenService
{
    /// <summary>
    /// Asynchronously generates a JSON Web Token (JWT) with specified claims and expiration time.
    /// </summary>
    /// <param name="secretKey">The secret key used to sign the JWT.</param>
    /// <param name="claims">A list of claims to include in the token payload, such as user roles or permissions.</param>
    /// <param name="expirationTime">The expiration time of the token, in minutes.</param>
    /// <param name="jti">Optional: A unique identifier for the token (JWT ID).</param>
    /// <param name="sub">Optional: The subject of the token, typically the user ID.</param>
    /// <param name="email">Optional: The email of the user associated with the token.</param>
    /// <returns>A task that represents the asynchronous operation, containing the generated JWT as a string.</returns>
    Task<string> GenerateToken(string secretKey, List<Claim>? claims, int expirationTime, string? jti = "",
        string? sub = "", string? email = "");

    /// <summary>
    /// Validates a JWT token using the specified secret key.
    /// </summary>
    /// <param name="secretKey">The secret key used for token validation.</param>
    /// <param name="token">The JWT token to validate.</param>
    /// <returns>A <see cref="ClaimsPrincipal"/> representing the result of the token validation.</returns>
    Task<ClaimsPrincipal> ValidateJwtToken(string secretKey, string token);
}