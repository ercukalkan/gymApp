using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Data.Entities;
using GymApp.NutritionService.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MassTransit.Initializers;

namespace GymApp.NutritionService.Core.Repositories;

public class FoodRepository(NutritionContext _context) : Repository<Food>(_context), IFoodRepository
{
    public async Task<IEnumerable<double>> GetCalories()
    {
        return await Context.Foods.Select(f => f.Calories).ToListAsync();
    }

    public async Task<IEnumerable<string?>> GetNames()
    {
        return await Context.Foods.Select(f => f.Name).ToListAsync();
    }
}