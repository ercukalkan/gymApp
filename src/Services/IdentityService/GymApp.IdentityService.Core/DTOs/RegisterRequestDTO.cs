namespace GymApp.IdentityService.Core.DTOs;

public class RegisterRequestDTO
{
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Gender { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}