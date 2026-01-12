using MassTransit;
using GymApp.Shared.MessageQueues.Events;

namespace GymApp.GymTrackingService.API.Features.EventPublishers;

public class WorkoutCompletedEventPublisher(ILogger<WorkoutCompletedEventPublisher> logger, IPublishEndpoint publishEndpoint)
{
    public async Task PublishWorkoutCompleted(Guid workoutId, int durationMinutes)
    {
        var @event = new WorkoutCompletedEvent
        {
            WorkoutId = workoutId,
            CompletedAt = DateTime.Now,
            DurationMinutes = durationMinutes
        };

        logger.LogInformation("WorkoutCompletedEvent was published");

        await publishEndpoint.Publish<WorkoutCompletedEvent>(@event);
    }
}