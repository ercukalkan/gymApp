using GymApp.NutritionService.Data.DTOs;
using GymApp.NutritionService.Data.Entities;

namespace GymApp.NutritionService.Core.Services.Interfaces;

public interface IFoodService : IService<Food>
{
    Task UpdateAsync(Guid id, FoodDTO dto);
    Task DeleteAsync(Guid id);
}