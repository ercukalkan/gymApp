using GymApp.Shared.Specification;

namespace GymApp.NutritionService.Core.Services.Interfaces;

public interface IService<T>
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec);
    Task<int> CountAsync(ISpecification<T> spec);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}