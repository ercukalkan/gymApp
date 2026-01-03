using GymApp.GymTrackingService.Data.Entities;

namespace GymApp.GymTrackingService.API.PublisherServices;

public interface IPublisherService
{
    Task PublishWorkoutCompletedEvent(Workout workout);
}