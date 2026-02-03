using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using GymApp.NutritionService.Core.Repositories.Interfaces;

namespace GymApp.NutritionService.Core.Repositories;

public class MealRepository(NutritionContext _context) : Repository<Meal>(_context), IMealRepository
{
    public async Task<Meal?> GetMealByIdAsync(Guid id)
    {
        return await GetByIdAsync(id);
    }

    public async Task<IEnumerable<Meal>> GetAllMealsAsync()
    {
        return await GetAllAsync();
    }

    public async Task AddMealAsync(Meal meal)
    {
        await AddAsync(meal);
    }

    public async Task UpdateMealAsync(Meal meal)
    {
        await UpdateAsync(meal);
    }

    public async Task DeleteMealAsync(Guid id)
    {
        await DeleteAsync(id);
    }
}