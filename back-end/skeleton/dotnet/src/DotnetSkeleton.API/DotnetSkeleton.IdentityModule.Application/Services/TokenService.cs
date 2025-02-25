using DotnetSkeleton.IdentityModule.Domain.Interfaces.Services;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DotnetSkeleton.IdentityModule.Application.Services;

public class TokenService : ITokenService
{
    #region Private Fields

    // Others
    private readonly ILogger<TokenService> _logger;

    // Options
    private readonly TokenOptions _tokenOptions;

    #endregion

    #region Constructor
    public TokenService(ILogger<TokenService> logger, IOptions<TokenOptions> tokenOptions)
    {
        _logger = logger;
        _tokenOptions = tokenOptions.Value;
    }

    #endregion

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
    public async Task<string> GenerateToken(string secretKey, List<Claim>? claims, int expirationTime, string? jti = null,
        string? sub = null, string? email = null)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);

        _logger.LogInformation("[GenerateToken] Generate a new JWT token");
        /*
        // Need to re-review this logic
        jti = jti ?? Guid.NewGuid().ToString();
        if (claims == null || !claims.Any())
        {
            claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Jti, jti),
                new (JwtRegisteredClaimNames.Sub, sub ?? string.Empty),
                new (JwtRegisteredClaimNames.Email, email ?? string.Empty)
            };
        }
        else
        {
            if (!claims.Exists(c => c.Properties.ContainsKey("Jti")))
            {
                claims.Add(new(JwtRegisteredClaimNames.Jti, jti));
            }
        }
        */

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(expirationTime),
            Issuer = _tokenOptions.TokenIssuer,
            Audience = _tokenOptions.TokenAudience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return await Task.FromResult(tokenHandler.WriteToken(token));
    }

    /// <summary>
    /// Validates a JWT token using the specified secret key.
    /// </summary>
    /// <param name="secretKey">The secret key used for token validation.</param>
    /// <param name="token">The JWT token to validate.</param>
    /// <returns>A <see cref="ClaimsPrincipal"/> representing the result of the token validation.</returns>
    public Task<ClaimsPrincipal> ValidateJwtToken(string secretKey, string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = _tokenOptions.TokenIssuer,
            ValidateAudience = true,
            ValidAudience = _tokenOptions.TokenAudience,
            ValidateLifetime = true
        };

        return Task.FromResult(tokenHandler.ValidateToken(token, tokenValidationParameters, out _));
    }
}