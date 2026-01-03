using MassTransit;
using GymApp.Shared.Events;
using GymApp.NutritionService.Data.Entities;

namespace GymApp.NutritionService.API.PublisherServices;

public class PublisherService(IPublishEndpoint publishEndPoint) : IPublisherService
{
    public async Task PublishFoodAddedEvent(Food food)
    {
        await publishEndPoint.Publish<IFoodAddedEvent>(new
        {
            FoodName = food.Name,
            DateAdded = DateTime.UtcNow,
            Calories = food.Calories
        });
    }
}