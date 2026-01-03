namespace GymApp.Shared.Events;

public interface IWorkoutCompletedEvent
{
    DateTime DateCompleted { get; }
    string CompleteMessage { get; }
}