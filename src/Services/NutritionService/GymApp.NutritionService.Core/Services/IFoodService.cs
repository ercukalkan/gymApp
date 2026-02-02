using GymApp.NutritionService.Data.Entities;

namespace GymApp.NutritionService.Core.Services;

public interface IFoodService
{
    Task<Food?> GetFoodByIdAsync(Guid id);
    Task<IEnumerable<Food>> GetAllFoodsAsync();
    Task AddFoodAsync(Food food);
    Task UpdateFoodAsync(Food food);
    Task DeleteFoodAsync(Guid id);
}