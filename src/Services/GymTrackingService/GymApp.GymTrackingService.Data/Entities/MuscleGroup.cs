using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GymApp.GymTrackingService.Data.Entities.JunctionEntities;

namespace GymApp.GymTrackingService.Data.Entities;

public class MuscleGroup
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [MaxLength(50)]
    public string? Name { get; set; }
    public ICollection<ExerciseMuscleGroup> Exercises { get; set; } = [];
}