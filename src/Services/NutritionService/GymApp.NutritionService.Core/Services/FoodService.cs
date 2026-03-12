using GymApp.NutritionService.Core.Repositories.Interfaces;
using GymApp.NutritionService.Data.Entities;
using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.NutritionService.Data.DTOs;

namespace GymApp.NutritionService.Core.Services;

public class FoodService(IFoodRepository _repository) : Service<Food, IFoodRepository>(_repository), IFoodService
{
    public async Task UpdateAsync(Guid id, FoodDTO dto)
    {
        await Repository.UpdateAsync(id, dto);
    }

    public async Task DeleteAsync(Guid id)
    {
        await Repository.DeleteAsync(id);
    }
}