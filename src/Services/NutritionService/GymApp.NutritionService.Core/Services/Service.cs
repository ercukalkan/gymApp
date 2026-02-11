using GymApp.NutritionService.Core.Repositories.Interfaces;
using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.Shared.Specification;

namespace GymApp.NutritionService.Core.Services;

public class Service<T>(IRepository<T> repository) : IService<T> where T : class
{
    public async Task CreateAsync(T entity)
    {
        await repository.AddAsync(entity);
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
}