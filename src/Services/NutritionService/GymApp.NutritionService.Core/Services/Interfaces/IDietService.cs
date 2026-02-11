using GymApp.NutritionService.Data.Entities;

namespace GymApp.NutritionService.Core.Services.Interfaces;

public interface IDietService
{
    Task<Diet?> GetDietByIdAsync(Guid id);
    Task AddDietAsync(Diet diet);
    Task UpdateDietAsync(Diet diet);
    Task DeleteDietAsync(Guid id);
}