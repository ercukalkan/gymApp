using GymApp.NutritionService.Core.Repositories.Interfaces;
using GymApp.NutritionService.Data.Context;
using GymApp.Shared.Specification;
using Microsoft.EntityFrameworkCore;

namespace GymApp.NutritionService.Core.Repositories;

public class Repository<TEntity>(NutritionContext _context) : IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = _context.Set<TEntity>();

    protected NutritionContext Context => _context;

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity> spec)
    {
        var source = SpecificationEvaluator<TEntity>.GetQuery(_dbSet, spec);

        return await source.ToListAsync();
    }

    public async Task<int> CountAsync(ISpecification<TEntity> spec)
    {
        var query = spec.ApplyWhereCriteria(_dbSet);

        return await query.CountAsync();
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