using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Specification;

namespace GymApp.NutritionService.Core.Services.Interfaces;

public interface IService<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsyncGeneric(ISpecification<T> spec);
    Task<T?> GetByIdAsyncGeneric(Guid id);
    Task<T> CreateAsyncGeneric(T entity);
    Task UpdateAsyncGeneric(T entity);
    Task DeleteAsyncGeneric(Guid id);
    Task<int> CountAsync(ISpecification<T> spec);
    Task<bool> IfExistsAsync(Guid id);
}