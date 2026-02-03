using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using GymApp.NutritionService.Core.Repositories.Interfaces;

namespace GymApp.NutritionService.Core.Repositories;

public class MealRepository(NutritionContext _context) : IMealRepository
{
    public async Task<Meal?> GetMealByIdAsync(Guid id)
    {
        return await _context.Meals.FindAsync(id);
    }

    public async Task<IEnumerable<Meal>> GetAllMealsAsync()
    {
        return await _context.Meals.ToListAsync();
    }

    public async Task AddMealAsync(Meal meal)
    {
        _context.Meals.Add(meal);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateMealAsync(Meal meal)
    {
        _context.Entry(meal).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMealAsync(Guid id)
    {
        var meal = await _context.Meals.FindAsync(id);
        if (meal != null)
        {
            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
        }
    }
}