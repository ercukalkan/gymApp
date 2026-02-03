using GymApp.NutritionService.Data.Entities;

namespace GymApp.NutritionService.Core.Repositories.Interfaces;

public interface IMealRepository
{
    Task<Meal?> GetMealByIdAsync(Guid id);
    Task<IEnumerable<Meal>> GetAllMealsAsync();
    Task AddMealAsync(Meal meal);
    Task UpdateMealAsync(Meal meal);
    Task DeleteMealAsync(Guid id);
}