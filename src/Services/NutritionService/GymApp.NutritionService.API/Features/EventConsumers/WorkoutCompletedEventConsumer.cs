using MassTransit;
using GymApp.Shared.MessageQueues.Events;

namespace GymApp.NutritionService.API.Features.EventConsumers;

public class WorkoutCompletedEventConsumer(ILogger<WorkoutCompletedEventConsumer> logger) : IConsumer<WorkoutCompletedEvent>
{
    public async Task Consume(ConsumeContext<WorkoutCompletedEvent> context)
    {
        var @event = context.Message;

        logger.LogInformation(
            "WorkoutCompleted event consumed: Id: {WorkoutId}, Duration: {DurationMinutes} minutes, Complete Date: {CompletedAt}.",
            @event.WorkoutId,
            @event.DurationMinutes,
            @event.CompletedAt
        );

        await Task.CompletedTask;
    }
}