using GymApp.NutritionService.Core.Repositories.Interfaces;
using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Specification;

namespace GymApp.NutritionService.Core.Services;

public class Service<T>(IRepository<T> repository) : IService<T> where T : BaseEntity
{
    public async Task<T?> GetByIdAsyncGeneric(Guid id)
    {
        return await repository.GetByIdAsyncGeneric(id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsyncGeneric(ISpecification<T> spec)
    {
        return await repository.GetAllAsyncGeneric(spec);
    }

    public async Task<T> CreateAsyncGeneric(T entity)
    {
        return await repository.AddAsyncGeneric(entity);
    }

    public async Task UpdateAsyncGeneric(T entity)
    {
        await repository.UpdateAsyncGeneric(entity);
    }

    public async Task DeleteAsyncGeneric(Guid id)
    {
        await repository.DeleteAsyncGeneric(id);
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        return await repository.CountAsync(spec);
    }

    public async Task<bool> IfExistsAsync(Guid id)
    {
        return await repository.IfExistsAsync(id);
    }
}