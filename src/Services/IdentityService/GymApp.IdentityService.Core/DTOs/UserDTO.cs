using GymApp.IdentityService.Data.Entities;

namespace GymApp.IdentityService.Core.DTOs;

public class UserDTO
{
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public IList<string> Roles { get; set; } = [];

    public static UserDTO CreateDTOFromUser(ApplicationUser user, IList<string> roles)
    {
        return new UserDTO
        {
            Id = user.Id,
            Username = user.UserName!,
            Email = user.Email!,
            FirstName = user.FirstName!,
            LastName = user.LastName!,
            PhoneNumber = user.PhoneNumber!,
            DateOfBirth = user.DateOfBirth,
            Roles = roles
        };
    }
}