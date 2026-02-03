using System.Linq.Expressions;
using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Pagination;

namespace GymApp.NutritionService.Core.Repositories.Interfaces;

public interface IFoodRepository : IRepository<Food>
{
    Task<IEnumerable<double>> GetCalories(double? minimum, double? maximum, string? sort, Pagination pagination);
    Task<IEnumerable<string?>> GetNames(string? sort, Pagination pagination);
    Task<IEnumerable<string?>> GetNamesStartsWith(Expression<Func<Food, bool>> expression);
    Task<IEnumerable<object?>> GetNamesAndCalories(Expression<Func<Food, bool>> expression);
}