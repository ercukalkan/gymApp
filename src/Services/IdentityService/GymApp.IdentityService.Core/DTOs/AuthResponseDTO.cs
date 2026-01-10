namespace GymApp.IdentityService.Core.DTOs;

public class AuthResponseDTO
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? UserName { get; set; }
}