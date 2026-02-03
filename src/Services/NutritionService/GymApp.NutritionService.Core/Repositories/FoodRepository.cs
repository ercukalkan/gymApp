using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymApp.NutritionService.Core.Repositories;

public class FoodRepository(NutritionContext _context) : IFoodRepository
{
    public async Task<Food?> GetFoodByIdAsync(Guid id)
    {
        return await _context.Foods.FindAsync(id);
    }

    public async Task<IEnumerable<Food>> GetAllFoodsAsync()
    {
        return await _context.Foods.ToListAsync();
    }

    public async Task AddFoodAsync(Food food)
    {
        _context.Foods.Add(food);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateFoodAsync(Food food)
    {
        _context.Entry(food).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteFoodAsync(Guid id)
    {
        var food = await _context.Foods.FindAsync(id);
        if (food != null)
        {
            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
        }
    }
}