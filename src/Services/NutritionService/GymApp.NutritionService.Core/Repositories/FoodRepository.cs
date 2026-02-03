using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Data.Entities;
using GymApp.NutritionService.Core.Repositories.Interfaces;

namespace GymApp.NutritionService.Core.Repositories;

public class FoodRepository(NutritionContext _context) : Repository<Food>(_context), IFoodRepository
{
    public async Task<Food?> GetFoodByIdAsync(Guid id)
    {
        return await GetByIdAsync(id);
    }

    public async Task<IEnumerable<Food>> GetAllFoodsAsync()
    {
        return await GetAllAsync();
    }

    public async Task AddFoodAsync(Food food)
    {
        await AddAsync(food);
    }

    public async Task UpdateFoodAsync(Food food)
    {
        await UpdateAsync(food);
    }

    public async Task DeleteFoodAsync(Guid id)
    {
        await DeleteAsync(id);
    }
}