using GymApp.NutritionService.Data.DTOs;
using GymApp.NutritionService.Data.Entities;

namespace GymApp.NutritionService.Core.Repositories.Interfaces;

public interface IFoodRepository : IRepository<Food>
{
    Task UpdateAsync(Guid id, FoodDTO dto);
    Task DeleteAsync(Guid id);
}