namespace GymApp.IdentityService.Core.DTOs;

public class LoginResponseDTO
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public int ExpiresIn { get; set; } // in minutes
    public UserDTO? User { get; set; }
}