using GymApp.NutritionService.Core.Repositories.Interfaces;
using GymApp.NutritionService.Data.Entities;
using GymApp.NutritionService.Core.Services.Interfaces;

namespace GymApp.NutritionService.Core.Services;

public class FoodService(IFoodRepository _repository) : Service<Food>(_repository), IFoodService
{

}