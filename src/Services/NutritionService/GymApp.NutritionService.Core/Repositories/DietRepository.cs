using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using GymApp.NutritionService.Core.Repositories.Interfaces;

namespace GymApp.NutritionService.Core.Repositories;

public class DietRepository(NutritionContext _context) : IDietRepository
{
    public async Task<Diet?> GetDietByIdAsync(Guid id)
    {
        return await _context.Diets.FindAsync(id);
    }

    public async Task<IEnumerable<Diet>> GetAllDietsAsync()
    {
        return await _context.Diets.ToListAsync();
    }

    public async Task AddDietAsync(Diet diet)
    {
        _context.Diets.Add(diet);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateDietAsync(Diet diet)
    {
        _context.Entry(diet).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteDietAsync(Guid id)
    {
        var diet = await _context.Diets.FindAsync(id);
        if (diet != null)
        {
            _context.Diets.Remove(diet);
            await _context.SaveChangesAsync();
        }
    }
}