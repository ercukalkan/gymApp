using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Data.Entities;
using GymApp.NutritionService.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MassTransit.Initializers;

namespace GymApp.NutritionService.Core.Repositories;

public class FoodRepository(NutritionContext _context) : Repository<Food>(_context), IFoodRepository
{
    public async Task<IEnumerable<double>> GetCalories(double? minimum, double? maximum)
    {
        var query = Context.Foods.Select(f => f.Calories);

        if (minimum.HasValue)
            query = query.Where(c => c >= minimum);

        if (maximum.HasValue)
            query = query.Where(c => c <= maximum);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<string?>> GetNames()
    {
        return await Context.Foods.Select(f => f.Name).ToListAsync();
    }
}