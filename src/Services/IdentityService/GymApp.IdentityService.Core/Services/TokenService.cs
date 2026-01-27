using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace GymApp.IdentityService.Core.Services
{
    public class TokenService(IConfiguration configuration, ILogger<TokenService> logger) : ITokenService
    {
        public string GenerateAccessToken(string userId, string email, string username, IList<string> roles)
        {
            try
            {
                var jwtSettings = configuration.GetSection("JwtSettings");
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));
                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new("userId", userId),
                    new("email", email),
                    new(ClaimTypes.Name, username),
                    new("isAdmin", roles.Contains("admin").ToString()),
                    new("jti", Guid.NewGuid().ToString())
                };

                foreach (var role in roles)
                {
                    claims.Add(new Claim("roles", role));
                }

                var token = new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(
                        int.Parse(jwtSettings["AccessTokenExpirationMinutes"]!)
                    ),
                    signingCredentials: credentials
                );

                var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

                logger.LogCritical("Access token generated for user {UserId}", userId);

                return accessToken;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error generating access token for user {UserId}", userId);
                throw;
            }
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            try
            {
                var jwtSettings = configuration.GetSection("JwtSettings");
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!)
                    ),
                    ValidateLifetime = false
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

                if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return null;
                }

                return principal;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error validating expired token");
                return null;
            }
        }
    }
}