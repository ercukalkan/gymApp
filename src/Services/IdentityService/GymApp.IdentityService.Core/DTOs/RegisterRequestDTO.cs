using System.ComponentModel.DataAnnotations;

namespace GymApp.IdentityService.Core.DTOs;

public class RegisterRequestDTO : IValidatableObject
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Username is required.")]
    [StringLength(15, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 15 characters.")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(15, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 15 characters.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirm Password is required.")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date of Birth is required.")]
    [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
    public DateTime? DateOfBirth { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public string? Gender { get; set; }

    [Required(ErrorMessage = "First Name is required.")]
    [StringLength(15, MinimumLength = 4, ErrorMessage = "First Name must be between 4 and 15 characters.")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required.")]
    [StringLength(15, MinimumLength = 4, ErrorMessage = "Last Name must be between 4 and 15 characters.")]
    public string? LastName { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DateOfBirth.HasValue)
        {
            var maxDate = DateTime.UtcNow.Date;
            if (DateOfBirth.Value.Date > maxDate)
            {
                yield return new ValidationResult(
                    "Date of birth must be today or earlier.",
                    [nameof(DateOfBirth)]
                );
            }
        }
    }
}