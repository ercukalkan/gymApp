using System.Linq.Expressions;
using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Pagination;

namespace GymApp.NutritionService.Core.Services.Interfaces;

public interface IFoodService
{
    Task<Food?> GetFoodByIdAsync(Guid id);
    Task<IEnumerable<Food>> GetAllFoodsAsync();
    Task AddFoodAsync(Food food);
    Task UpdateFoodAsync(Food food);
    Task DeleteFoodAsync(Guid id);
    Task<IEnumerable<double>> GetCalories(double? minimum, double? maximum, PaginationParams pagination);
    Task<IEnumerable<string?>> GetNames(PaginationParams pagination);
    Task<IEnumerable<string?>> GetNamesStartsWith(Expression<Func<Food, bool>> expression);
}