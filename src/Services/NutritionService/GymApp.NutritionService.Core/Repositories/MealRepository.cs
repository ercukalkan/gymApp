using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Data.Entities;
using GymApp.NutritionService.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using GymApp.Shared.Specification;
using GymApp.NutritionService.Data.DTOs;
using GymApp.NutritionService.Data.Entities.JunctionEntities;
using System.ComponentModel.DataAnnotations;

namespace GymApp.NutritionService.Core.Repositories;

public class MealRepository(NutritionContext _context) : Repository<Meal>(_context), IMealRepository
{
    public async Task<IReadOnlyList<MealResponseDTO>> GetAllAsync(ISpecification<Meal> spec)
    {
        var source = SpecificationEvaluator<Meal>.GetQuery(Context.Meals
            .Include(m => m.MealFoods)
            .ThenInclude(mf => mf.Food), spec);

        var result = source.Select(m => MealResponseDTO.FromEntity(m)).AsNoTracking();

        return await result.ToListAsync();
    }

    public async Task<MealResponseDTO> GetByIdAsync(Guid id)
    {
        var meal = await Context.Meals
            .Include(m => m.MealFoods)
            .ThenInclude(mf => mf.Food)
            .FirstOrDefaultAsync(m => m.Id == id)
            ?? throw new KeyNotFoundException($"Meal not found with id {id}");

        return MealResponseDTO.FromEntity(meal);
    }

    public async Task<MealResponseDTO> CreateAsync(MealRequestDTO dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        if (string.IsNullOrEmpty(dto.Name))
            throw new ValidationException("Object name is required.");

        var newMeal = new Meal
        {
            Name = dto.Name,
            MealFoods = [.. dto.MealFoodIds.Select(i => new MealFood { FoodId = i })]
        };

        var entityEntry = await Context.Meals.AddAsync(newMeal);
        await Context.SaveChangesAsync();

        var addedMeal = await Context.Meals
            .Include(m => m.MealFoods)
            .ThenInclude(mf => mf.Food)
            .FirstOrDefaultAsync(m => m.Id == newMeal.Id)
            ?? throw new KeyNotFoundException("New meal not found.");

        return MealResponseDTO.FromEntity(addedMeal);
    }

    public async Task UpdateAsync(Guid id, MealRequestDTO dto)
    {
        var existingMeal = await Context.Meals
            .Include(m => m.MealFoods)
            .ThenInclude(mf => mf.Food)
            .FirstOrDefaultAsync(m => m.Id == id)
            ?? throw new InvalidOperationException($"No existing Meal found with Id: {id}");

        existingMeal.Name = dto.Name;

        var existingMealFoodIds = existingMeal.MealFoods
            .Select(mf => mf.FoodId)
            .ToHashSet();

        var dtoMealFoodIds = dto.MealFoodIds
            .ToHashSet();

        var foodsToRemove = existingMeal.MealFoods
            .Where(mf => !dtoMealFoodIds.Contains(mf.FoodId))
            .ToHashSet();

        var foodsToAdd = dtoMealFoodIds
            .Where(mf => !existingMealFoodIds.Contains(mf))
            .ToHashSet();

        foreach (var mealfood in foodsToRemove)
        {
            existingMeal.MealFoods.Remove(mealfood);
        }

        foreach (var foodId in foodsToAdd)
        {
            existingMeal.MealFoods.Add(new MealFood { FoodId = foodId });
        }

        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var meal = await Context.Meals.FindAsync(id)
            ?? throw new NullReferenceException($"No Meal found with Id {id}");

        Context.Meals.Remove(meal);
        await Context.SaveChangesAsync();
    }
}