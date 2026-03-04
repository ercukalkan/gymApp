using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Specification;

namespace GymApp.NutritionService.Core.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IReadOnlyList<TEntity>> GetAllAsyncGeneric(ISpecification<TEntity> spec);
    Task<TEntity?> GetByIdAsyncGeneric(Guid id);
    Task<TEntity> AddAsyncGeneric(TEntity entity);
    Task UpdateAsyncGeneric(TEntity entity);
    Task DeleteAsyncGeneric(Guid id);
    Task<int> CountAsync(ISpecification<TEntity> spec);
    Task<bool> IfExistsAsync(Guid id);
}