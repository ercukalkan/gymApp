using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Data.Entities;
using GymApp.NutritionService.Core.Repositories.Interfaces;
using GymApp.NutritionService.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GymApp.NutritionService.Core.Repositories;

public class FoodRepository(NutritionContext _context) : Repository<Food>(_context), IFoodRepository
{
    public async Task UpdateAsync(Guid id, FoodDTO dto)
    {
        Food? existingFood = await Context.Foods.FindAsync(id)
            ?? throw new InvalidOperationException($"No existing Food found with Id: {id}");

        existingFood.Name = dto.Name ?? "Autoname by app";

        existingFood.Calories = dto.Calories;
        existingFood.Carbohydrates = dto.Carbohydrates;
        existingFood.Protein = dto.Protein;
        existingFood.Fats = dto.Fats;

        List<Guid> corrMFMealIds = [.. Context.MealFoods.Where(mf => mf.FoodId == id).Select(mf => mf.MealId)];

        List<Meal> corrMeals = [..
            Context.Meals
            .Include(m => m.MealFoods)
            .ThenInclude(mf => mf.Food)
            .Where(m => corrMFMealIds.Contains(m.Id))
        ];

        foreach (Meal meal in corrMeals)
        {
            meal.RecalculateNutrients();
        }

        await Context.SaveChangesAsync();
    }
}