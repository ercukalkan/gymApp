using GymApp.NutritionService.Data.Entities;

namespace GymApp.NutritionService.Core.Services.Interfaces;

public interface IFoodService
{
    Task<Food?> GetFoodByIdAsync(Guid id);
    Task<IEnumerable<Food>> GetAllFoodsAsync();
    Task AddFoodAsync(Food food);
    Task UpdateFoodAsync(Food food);
    Task DeleteFoodAsync(Guid id);
    Task<IEnumerable<double>> GetCalories(double? minimum, double? maximum);
    Task<IEnumerable<string?>> GetNames();
}