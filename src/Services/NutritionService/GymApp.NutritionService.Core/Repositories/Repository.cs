using GymApp.NutritionService.Core.Repositories.Interfaces;
using GymApp.NutritionService.Data.Context;
using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Specification;
using Microsoft.EntityFrameworkCore;

namespace GymApp.NutritionService.Core.Repositories;

public class Repository<TEntity>(NutritionContext _context) : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet = _context.Set<TEntity>();

    protected NutritionContext Context => _context;

    public async Task<IReadOnlyList<TEntity>> GetAllAsyncGeneric(ISpecification<TEntity> spec)
    {
        var source = SpecificationEvaluator<TEntity>.GetQuery(_dbSet, spec);

        return await source.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsyncGeneric(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<TEntity> AddAsyncGeneric(TEntity entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsyncGeneric(TEntity entity)
    {
        _dbSet.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsyncGeneric(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> CountAsync(ISpecification<TEntity> spec)
    {
        var query = spec.ApplyWhereCriteria(_dbSet);

        return await query.CountAsync();
    }

    public async Task<bool> IfExistsAsync(Guid id)
    {
        return await _dbSet.AnyAsync(e => e.Id == id);
    }
}