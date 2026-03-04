using GymApp.NutritionService.Data.DTOs;
using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Specification;

namespace GymApp.NutritionService.Core.Services.Interfaces;

public interface IMealService : IService<Meal>
{
    Task<IReadOnlyList<MealDTO>> GetAllAsync(ISpecification<Meal> spec);
}