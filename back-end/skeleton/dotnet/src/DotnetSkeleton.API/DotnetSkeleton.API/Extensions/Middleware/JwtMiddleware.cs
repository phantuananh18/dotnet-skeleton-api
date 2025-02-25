using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using DotnetSkeleton.Core.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DotnetSkeleton.API.Extensions.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenOptions _tokenOptions;

        public JwtMiddleware(RequestDelegate next, IOptions<TokenOptions> tokenOptions)
        {
            _next = next;
            _tokenOptions = tokenOptions.Value;
        }

        public async Task Invoke(HttpContext context, IUserRepository userRepository)
        {
            if (context.Request.Headers.TryGetValue(Constant.HeaderAttribute.Authorization, out StringValues value))
            {
                var tokenPrefix = value.FirstOrDefault()?.Split(" ").First();
                var token = value.FirstOrDefault()?.Split(" ").Last();
                if (tokenPrefix == Constant.AuthenticateAttribute.BearerTokenPrefix && !string.IsNullOrEmpty(token))
                {
                    var jwtSecurityToken = await ValidateToken(token);
                    if (jwtSecurityToken == null)
                    {
                        // If the request is not authorized, return 401 Unauthorized
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return;
                    }

                    // Claims UserId from JWT Token
                    var userId = int.Parse(jwtSecurityToken.Claims.First(x => x.Type == Constant.AuthenticateAttribute.Id).Value);
                    var existingUser = await userRepository.GetUserProfileDataByIdAsync(userId);
                    if (existingUser != null)
                    {
                        // Attach user to context on successful jwt validation
                        context.Items[Constant.FieldName.User] = existingUser;
                    }
                }
            }

            await _next(context);
        }

        private Task<JwtSecurityToken> ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenOptions.JwtSecretKey);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return Task.FromResult((JwtSecurityToken)validatedToken);
        }
    }
}
