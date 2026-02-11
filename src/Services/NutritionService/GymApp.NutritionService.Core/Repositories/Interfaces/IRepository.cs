using GymApp.Shared.Specification;

namespace GymApp.NutritionService.Core.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity> spec);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(Guid id);
    Task<int> CountAsync(ISpecification<TEntity> spec);
}