using GymApp.Shared.Events;
using MassTransit;
using System.Text.Json;

namespace GymApp.NutritionService.API.EventConsumers;

public class WorkoutCompletedEventConsumer : IConsumer<IWorkoutCompletedEvent>
{
    public async Task Consume(ConsumeContext<IWorkoutCompletedEvent> context)
    {
        var serializedMessage = JsonSerializer.Serialize(context.Message, new JsonSerializerOptions { });

        // var serializedMessage = context.Message.ToString();

        Console.WriteLine($"WorkoutCompleted event consumed. Message: {serializedMessage}");

        await Task.CompletedTask;
    }
}