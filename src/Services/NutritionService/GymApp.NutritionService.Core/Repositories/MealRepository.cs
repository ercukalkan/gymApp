using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Data.Entities;
using GymApp.NutritionService.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using GymApp.NutritionService.Data.Entities.JunctionEntities;

namespace GymApp.NutritionService.Core.Repositories;

public class MealRepository(NutritionContext _context) : Repository<Meal>(_context), IMealRepository
{
    public override async Task<Meal?> GetByIdAsync(Guid id)
    {
        return await Context.Meals
            .Include(m => m.MealFoods)
            .ThenInclude(mf => mf.Food)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public override async Task<Meal> AddAsync(Meal entity)
    {
        if (entity == null)
            throw new NullReferenceException("Meal entity is null.");

        var newMeal = new Meal()
        {
            Name = entity.Name,
            MealFoods = [.. entity.MealFoods.Select(mf => new MealFood() { FoodId = mf.FoodId })]
        };

        await Context.Meals.AddAsync(newMeal);
        await Context.SaveChangesAsync();

        return newMeal;
    }

    public override async Task UpdateAsync(Meal entity)
    {
        var existingMeal = await GetByIdAsync(entity.Id)
            ?? throw new InvalidOperationException($"Meal with ID {entity.Id} not found.");

        existingMeal.Name = entity.Name;

        var mealFoodIds = existingMeal.MealFoods.Select(mf => mf.FoodId).ToList();
        var entityFoodIds = entity.MealFoods.Select(mf => mf.FoodId).ToList();

        var foodsToRemove = existingMeal.MealFoods.Where(mf => !entityFoodIds.Contains(mf.FoodId)).ToList();
        var foodsToAdd = entity.MealFoods.Where(mf => !mealFoodIds.Contains(mf.FoodId)).ToList();

        foodsToRemove.ForEach(mf => existingMeal.MealFoods.Remove(mf));
        foodsToAdd.ForEach(existingMeal.MealFoods.Add);

        await Context.SaveChangesAsync();
    }
}