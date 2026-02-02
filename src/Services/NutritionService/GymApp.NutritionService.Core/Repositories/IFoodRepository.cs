using GymApp.NutritionService.Data.Entities;

namespace GymApp.NutritionService.Core.Repositories;

public interface IFoodRepository
{
    Task<Food?> GetFoodByIdAsync(Guid id);
    Task<IEnumerable<Food>> GetAllFoodsAsync();
    Task AddFoodAsync(Food food);
    Task UpdateFoodAsync(Food food);
    Task DeleteFoodAsync(Guid id);
}