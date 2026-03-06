using GymApp.NutritionService.Core.Repositories.Interfaces;
using GymApp.NutritionService.Data.Entities;
using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.Shared.Specification;
using GymApp.NutritionService.Data.DTOs;

namespace GymApp.NutritionService.Core.Services;

public class MealService(IMealRepository _repository) : Service<Meal, IMealRepository>(_repository), IMealService
{
    public async Task<IReadOnlyList<MealResponseDTO>> GetAllAsync(ISpecification<Meal> spec)
    {
        return await Repository.GetAllAsync(spec);
    }

    public async Task<MealResponseDTO> GetByIdAsync(Guid id)
    {
        return await Repository.GetByIdAsync(id);
    }

    public async Task<MealResponseDTO> CreateAsync(MealRequestDTO dto)
    {
        return await Repository.CreateAsync(dto);
    }

    public async Task UpdateAsync(Guid id, MealRequestDTO dto)
    {
        await Repository.UpdateAsync(id, dto);
    }

    public async Task DeleteAsync(Guid id)
    {
        await Repository.DeleteAsync(id);
    }
}