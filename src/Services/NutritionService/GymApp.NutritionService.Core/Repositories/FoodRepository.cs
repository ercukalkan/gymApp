using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Data.Entities;
using GymApp.NutritionService.Core.Repositories.Interfaces;
using GymApp.NutritionService.Data.DTOs;
using Microsoft.EntityFrameworkCore;
using GymApp.NutritionService.Data.Entities.JunctionEntities;

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

        List<Guid> affectedMealIds = [.. Context.MealFoods.Where(mf => mf.FoodId == id).Select(mf => mf.MealId)];

        List<Meal> affectedMeals = [..
            Context.Meals
            .Include(m => m.MealFoods)
            .ThenInclude(mf => mf.Food)
            .Where(m => affectedMealIds.Contains(m.Id))
        ];

        foreach (Meal meal in affectedMeals)
        {
            meal.RecalculateNutrients();
        }

        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        Food foodToDelete = await Context.Foods.FindAsync(id)
         ?? throw new NullReferenceException($"No Food found with Id {id}");

        List<Guid> affectedMealIds = await Context.MealFoods
            .Where(mf => mf.FoodId == id)
            .Select(mf => mf.MealId)
            .Distinct()
            .ToListAsync();

        List<Meal> affectedMeals = await Context.Meals
            .Include(m => m.MealFoods)
            .ThenInclude(mf => mf.Food)
            .Where(m => affectedMealIds.Contains(m.Id))
            .ToListAsync();

        foreach (Meal meal in affectedMeals)
        {
            MealFood mealFoodToRemove = meal.MealFoods.FirstOrDefault(mf => mf.FoodId == id)
                ?? throw new NullReferenceException($"No MealFood found under {meal.Name} with Id {id}");

            meal.MealFoods.Remove(mealFoodToRemove);
            meal.RecalculateNutrients();
        }

        Context.Remove(foodToDelete);

        await Context.SaveChangesAsync();
    }
}