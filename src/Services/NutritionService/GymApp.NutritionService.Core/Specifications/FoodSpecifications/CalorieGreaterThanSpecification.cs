using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Specification;

namespace GymApp.NutritionService.Core.Specifications.FoodSpecifications;

public class CalorieGreaterThanSpecification(double value) : GenericSpecification<Food>(f => f.Calories >= value)
{

}