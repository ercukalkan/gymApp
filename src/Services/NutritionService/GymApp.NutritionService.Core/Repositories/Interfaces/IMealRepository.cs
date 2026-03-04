using GymApp.NutritionService.Data.DTOs;
using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Specification;

namespace GymApp.NutritionService.Core.Repositories.Interfaces;

public interface IMealRepository : IRepository<Meal>
{
    Task<IReadOnlyList<MealDTO>> GetAllAsync(ISpecification<Meal> spec);
}