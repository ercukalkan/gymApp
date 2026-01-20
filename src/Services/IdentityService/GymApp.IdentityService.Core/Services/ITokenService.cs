using System.Security.Claims;

namespace GymApp.IdentityService.Core.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(string userId, string email, string username, IList<string> roles);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}