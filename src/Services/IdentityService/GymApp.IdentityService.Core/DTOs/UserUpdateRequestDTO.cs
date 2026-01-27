namespace GymApp.IdentityService.Core.DTOs;

public class UserUpdateRequestDTO
{
    public UserDTO UserDTO { get; set; } = null!;
    public List<string> Roles { get; set; } = [];
}