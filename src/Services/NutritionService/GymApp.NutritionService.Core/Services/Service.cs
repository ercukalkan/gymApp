using GymApp.NutritionService.Core.Repositories.Interfaces;
using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Specification;

namespace GymApp.NutritionService.Core.Services;

public class Service<TEntity, TRepository>(TRepository repository) : IService<TEntity>
where TEntity : BaseEntity
where TRepository : IRepository<TEntity>
{
    protected TRepository Repository => repository;

    public async Task<TEntity?> GetByIdAsyncGeneric(Guid id)
    {
        return await repository.GetByIdAsyncGeneric(id);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsyncGeneric(ISpecification<TEntity> spec)
    {
        return await repository.GetAllAsyncGeneric(spec);
    }

    public async Task<TEntity> CreateAsyncGeneric(TEntity entity)
    {
        return await repository.AddAsyncGeneric(entity);
    }

    public async Task UpdateAsyncGeneric(TEntity entity)
    {
        await repository.UpdateAsyncGeneric(entity);
    }

    public async Task DeleteAsyncGeneric(Guid id)
    {
        await repository.DeleteAsyncGeneric(id);
    }

    public async Task<int> CountAsync(ISpecification<TEntity> spec)
    {
        return await repository.CountAsync(spec);
    }

    public async Task<bool> IfExistsAsync(Guid id)
    {
        return await repository.IfExistsAsync(id);
    }
}