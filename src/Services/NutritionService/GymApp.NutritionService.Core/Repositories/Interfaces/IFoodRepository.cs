using GymApp.NutritionService.Data.Entities;

namespace GymApp.NutritionService.Core.Repositories.Interfaces;

public interface IFoodRepository : IRepository<Food>
{
    Task<IEnumerable<double>> GetCalories();
    Task<IEnumerable<string?>> GetNames();
}