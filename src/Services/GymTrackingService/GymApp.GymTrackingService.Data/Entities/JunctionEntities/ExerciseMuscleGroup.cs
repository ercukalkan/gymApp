using System.ComponentModel.DataAnnotations.Schema;

namespace GymApp.GymTrackingService.Data.Entities.JunctionEntities;

public class ExerciseMuscleGroup
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid ExerciseId { get; set; }
    public Exercise? Exercise { get; set; } = null!;

    public Guid MuscleGroupId { get; set; }
    public MuscleGroup? MuscleGroup { get; set; } = null!;
}