using GymApp.NutritionService.Core.Repositories.Interfaces;
using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Specification;

namespace GymApp.NutritionService.Core.Services;

public class Service<T>(IRepository<T> repository) : IService<T> where T : BaseEntity
{
    public async Task<T> CreateAsync(T entity)
    {
        return await repository.AddAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await repository.DeleteAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec)
    {
        return await repository.GetAllAsync(spec);
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        return await repository.CountAsync(spec);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        await repository.UpdateAsync(entity);
    }

    public async Task<bool> IfExistsAsync(Guid id)
    {
        return await repository.IfExistsAsync(id);
    }
}