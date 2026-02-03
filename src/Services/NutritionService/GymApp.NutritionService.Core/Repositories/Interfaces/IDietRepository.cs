using GymApp.NutritionService.Data.Entities;

namespace GymApp.NutritionService.Core.Repositories.Interfaces;

public interface IDietRepository : IRepository<Diet>
{
    Task<Diet?> GetDietByIdAsync(Guid id);
    Task<IEnumerable<Diet>> GetAllDietsAsync();
    Task AddDietAsync(Diet diet);
    Task UpdateDietAsync(Diet diet);
    Task DeleteDietAsync(Guid id);
}