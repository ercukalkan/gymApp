using System.Linq.Expressions;
using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Pagination;
using GymApp.Shared.Specification;

namespace GymApp.NutritionService.Core.Services.Interfaces;

public interface IFoodService
{
    Task<Food?> GetFoodByIdAsync(Guid id);
    Task<IEnumerable<Food>> GetAllFoodsAsync();
    Task AddFoodAsync(Food food);
    Task UpdateFoodAsync(Food food);
    Task DeleteFoodAsync(Guid id);
    Task<IEnumerable<double>> GetCalories(double? minimum, double? maximum, string? sort, Pagination pagination);
    Task<IEnumerable<string?>> GetNames(string? sort, Pagination pagination);
    Task<IEnumerable<string?>> GetNamesStartsWith(Expression<Func<Food, bool>> expression);
    Task<IEnumerable<object?>> GetNamesAndCalories(Specification<Food> specification);
}