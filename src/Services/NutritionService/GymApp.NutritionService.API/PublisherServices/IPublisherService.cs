using GymApp.NutritionService.Data.Entities;

namespace GymApp.NutritionService.API.PublisherServices;

public interface IPublisherService
{
    Task PublishFoodAddedEvent(Food food);
}