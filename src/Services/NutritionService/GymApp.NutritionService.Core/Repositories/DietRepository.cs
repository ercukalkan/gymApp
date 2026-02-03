using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using GymApp.NutritionService.Core.Repositories.Interfaces;

namespace GymApp.NutritionService.Core.Repositories;

public class DietRepository(NutritionContext _context) : Repository<Diet>(_context), IDietRepository
{
    public async Task<Diet?> GetDietByIdAsync(Guid id)
    {
        return await GetByIdAsync(id);
    }

    public async Task<IEnumerable<Diet>> GetAllDietsAsync()
    {
        return await GetAllAsync();
    }

    public async Task AddDietAsync(Diet diet)
    {
        await AddAsync(diet);
    }

    public async Task UpdateDietAsync(Diet diet)
    {
        await UpdateAsync(diet);
    }

    public async Task DeleteDietAsync(Guid id)
    {
        await DeleteAsync(id);
    }
}