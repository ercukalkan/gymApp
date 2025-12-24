using System.ComponentModel.DataAnnotations.Schema;

namespace GymApp.GymTrackingService.Data.Entities.JunctionEntities;

public class WorkoutExercise
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid WorkoutId { get; set; }
    public Workout? Workout { get; set; } = null!;

    public Guid ExerciseId { get; set; }
    public Exercise? Exercise { get; set; } = null!;
}