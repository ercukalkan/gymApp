using GymApp.NutritionService.Data.DTOs;
using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Specification;

namespace GymApp.NutritionService.Core.Repositories.Interfaces;

public interface IMealRepository : IRepository<Meal>
{
    Task<IReadOnlyList<MealResponseDTO>> GetAllAsync(ISpecification<Meal> spec);
    Task<MealResponseDTO> GetByIdAsync(Guid id);
    Task<MealResponseDTO> CreateAsync(MealRequestDTO dto);
    Task UpdateAsync(Guid id, MealRequestDTO dto);
    Task DeleteAsync(Guid id);
}