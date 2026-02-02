using GymApp.NutritionService.Core.Caching;
using GymApp.NutritionService.Core.Repositories;
using GymApp.NutritionService.Data.Entities;

namespace GymApp.NutritionService.Core.Services;

public class FoodService(IFoodRepository _repository, IRedisService _redisService) : IFoodService
{
    public async Task<Food?> GetFoodByIdAsync(Guid id)
    {
        var cachedFood = await _redisService.GetAsync<Food>(id.ToString());
        if (cachedFood != null)
        {
            return cachedFood;
        }

        var food = await _repository.GetFoodByIdAsync(id);
        if (food != null)
        {
            await _redisService.SetAsync(id.ToString(), food, TimeSpan.FromSeconds(20));
        }

        return food;
    }

    public async Task<IEnumerable<Food>> GetAllFoodsAsync()
    {
        var cachedFoods = await _redisService.GetAsync<IEnumerable<Food>>("all_foods");
        if (cachedFoods != null)
        {
            return cachedFoods;
        }

        var foods = await _repository.GetAllFoodsAsync();
        await _redisService.SetAsync("all_foods", foods, TimeSpan.FromSeconds(20));
        return foods;
    }

    public async Task AddFoodAsync(Food food)
    {
        await _repository.AddFoodAsync(food);
    }

    public async Task UpdateFoodAsync(Food food)
    {
        await _repository.UpdateFoodAsync(food);
    }

    public async Task DeleteFoodAsync(Guid id)
    {
        await _repository.DeleteFoodAsync(id);
        await _redisService.RemoveAsync(id.ToString());
    }
}