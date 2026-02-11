using GymApp.NutritionService.Core.Repositories.Interfaces;
using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.Shared.Specification;

namespace GymApp.NutritionService.Core.Services;

public class Service<T>(IRepository<T> repository) : IService<T> where T : class
{
    public Task<T> CreateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec)
    {
        return await repository.GetAllAsync(spec);
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        return await repository.CountAsync(spec);
    }

    public Task<T> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<T> UpdateAsync(string id, T entity)
    {
        throw new NotImplementedException();
    }
}