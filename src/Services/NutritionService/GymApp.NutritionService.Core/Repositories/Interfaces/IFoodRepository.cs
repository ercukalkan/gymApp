using GymApp.NutritionService.Data.Entities;

namespace GymApp.NutritionService.Core.Repositories.Interfaces;

public interface IFoodRepository : IRepository<Food>
{
    Task<IEnumerable<double>> GetCalories(double? minimum, double? maximum, string? sort);
    Task<IEnumerable<string?>> GetNames(string? sort);
}