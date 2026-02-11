using GymApp.NutritionService.Data.Entities;

namespace GymApp.NutritionService.Core.Services.Interfaces;

public interface IMealService
{
    Task<Meal?> GetMealByIdAsync(Guid id);
    Task AddMealAsync(Meal meal);
    Task UpdateMealAsync(Meal meal);
    Task DeleteMealAsync(Guid id);
}