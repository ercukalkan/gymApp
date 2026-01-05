namespace GymApp.Shared.MessageQueues.Events;

public record WorkoutCompletedEvent
{
    public Guid WorkoutId { get; set; }
    // public string? ExerciseNames { get; set; }
    public DateTime CompletedAt { get; set; }
    public int DurationMinutes { get; set; }
}