using System.ComponentModel.DataAnnotations.Schema;
using GymApp.GymTrackingService.Data.Entities.JunctionEntities;

namespace GymApp.GymTrackingService.Data.Entities;

public class Workout
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public ICollection<WorkoutExercise> Exercises { get; set; } = [];
}