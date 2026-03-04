using GymApp.NutritionService.Core.Repositories.Interfaces;
using GymApp.NutritionService.Data.Entities;
using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.Shared.Specification;
using GymApp.NutritionService.Data.DTOs;

namespace GymApp.NutritionService.Core.Services;

public class MealService(IMealRepository _repository) : Service<Meal>(_repository), IMealService
{
    public async Task<IReadOnlyList<MealDTO>> GetAllAsync(ISpecification<Meal> spec)
    {
        return await _repository.GetAllAsync(spec);
    }
}