using GymApp.NutritionService.Core.Caching;
using GymApp.NutritionService.Core.Repositories.Interfaces;
using GymApp.NutritionService.Data.Entities;
using GymApp.NutritionService.Core.Services.Interfaces;

namespace GymApp.NutritionService.Core.Services;

public class MealService(IMealRepository _repository, IRedisService _redisService) : IMealService
{
    public async Task<Meal?> GetMealByIdAsync(Guid id)
    {
        var cachedMeal = await _redisService.GetAsync<Meal>(id.ToString());
        if (cachedMeal != null)
        {
            return cachedMeal;
        }

        var meal = await _repository.GetMealByIdAsync(id);
        if (meal != null)
        {
            await _redisService.SetAsync(id.ToString(), meal, TimeSpan.FromSeconds(20));
        }

        return meal;
    }

    public async Task<IEnumerable<Meal>> GetAllMealsAsync()
    {
        var cachedMeals = await _redisService.GetAsync<IEnumerable<Meal>>("all_meals");
        if (cachedMeals != null)
        {
            return cachedMeals;
        }

        var meals = await _repository.GetAllMealsAsync();
        await _redisService.SetAsync("all_meals", meals, TimeSpan.FromSeconds(20));
        return meals;
    }

    public async Task AddMealAsync(Meal meal)
    {
        await _repository.AddMealAsync(meal);
    }

    public async Task UpdateMealAsync(Meal meal)
    {
        await _repository.UpdateMealAsync(meal);
    }

    public async Task DeleteMealAsync(Guid id)
    {
        await _repository.DeleteMealAsync(id);
        await _redisService.RemoveAsync(id.ToString());
    }
}