using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Data.Entities;
using GymApp.NutritionService.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using GymApp.Shared.Specification;
using GymApp.NutritionService.Data.DTOs;

namespace GymApp.NutritionService.Core.Repositories;

public class MealRepository(NutritionContext _context) : Repository<Meal>(_context), IMealRepository
{
    public async Task<IReadOnlyList<MealDTO>> GetAllAsync(ISpecification<Meal> spec)
    {
        var source = SpecificationEvaluator<Meal>.GetQuery(Context.Meals
            .Include(m => m.MealFoods)
            .ThenInclude(mf => mf.Food), spec);

        var result = source.Select(m => new MealDTO
        {
            Id = m.Id,
            Name = m.Name,
            Calories = m.Calories,
            Carbohydrates = m.Carbohydrates,
            Protein = m.Protein,
            Fats = m.Fats,
            MealFoods = m.MealFoods.Select(mf => new NameDTO
            {
                Name = mf.Food.Name
            })
        })
        .AsNoTracking();

        return await result.ToListAsync();
    }
}