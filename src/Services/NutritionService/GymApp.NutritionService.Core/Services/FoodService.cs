using GymApp.NutritionService.Core.Caching;
using GymApp.NutritionService.Core.Repositories.Interfaces;
using GymApp.NutritionService.Data.Entities;
using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.Shared.Pagination;
using System.Linq.Expressions;

namespace GymApp.NutritionService.Core.Services;

public class FoodService(IFoodRepository _repository, IRedisService _redisService) : Service<Food>(_repository), IFoodService
{
    public async Task<Food?> GetFoodByIdAsync(Guid id)
    {
        var cachedFood = await _redisService.GetAsync<Food>(id.ToString());
        if (cachedFood != null)
        {
            return cachedFood;
        }

        var food = await _repository.GetByIdAsync(id);
        if (food != null)
        {
            await _redisService.SetAsync(id.ToString(), food, TimeSpan.FromSeconds(20));
        }

        return food;
    }

    public async Task AddFoodAsync(Food food)
    {
        await _repository.AddAsync(food);
    }

    public async Task UpdateFoodAsync(Food food)
    {
        await _repository.UpdateAsync(food);
    }

    public async Task DeleteFoodAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
        await _redisService.RemoveAsync(id.ToString());
    }

    public async Task<IEnumerable<double>> GetCalories(double? minimum, double? maximum, PaginationParams pagination)
    {
        return await _repository.GetCalories(minimum, maximum, pagination);
    }

    public async Task<IEnumerable<string?>> GetNames(PaginationParams pagination)
    {
        return await _repository.GetNames(pagination);
    }

    public async Task<IEnumerable<string?>> GetNamesStartsWith(Expression<Func<Food, bool>> expression)
    {
        return await _repository.GetNamesStartsWith(expression);
    }
}