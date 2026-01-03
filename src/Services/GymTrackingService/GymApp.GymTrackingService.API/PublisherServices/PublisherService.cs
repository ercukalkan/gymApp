using GymApp.GymTrackingService.Data.Entities;
using GymApp.Shared.Events;
using MassTransit;

namespace GymApp.GymTrackingService.API.PublisherServices;

public class PublisherService(IBus bus) : IPublisherService
{
    public async Task PublishWorkoutCompletedEvent(Workout workout)
    {
        await bus.Publish<IWorkoutCompletedEvent>(new
        {
            DateCompleted = workout.Date,
            CompleteMessage = "Workout completed."
        });
    }
}