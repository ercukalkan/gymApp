using GymApp.NutritionService.Core.Repositories.Interfaces;
using GymApp.NutritionService.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GymApp.NutritionService.Core.Repositories;

public class Repository<TEntity>(NutritionContext _context) : IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = _context.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}