using System.ComponentModel.DataAnnotations;

namespace GymApp.GymTrackingService.Data.Entities;

public class Student
{
    [Key]
    public string UserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime EnrollmentDate { get; set; }

    public Trainer? Trainer { get; set; }
}