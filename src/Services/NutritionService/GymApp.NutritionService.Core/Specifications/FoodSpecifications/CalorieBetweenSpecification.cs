using System.Linq.Expressions;
using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Specification;

namespace GymApp.NutritionService.Core.Specifications.FoodSpecifications;

public class CalorieBetweenSpecification(double? min, double? max) : Specification<Food>
{
    public override Expression<Func<Food, bool>>? ToExpression()
    {
        return food =>
            (!min.HasValue || food.Calories >= min) && (!max.HasValue || food.Calories <= max);
    }
}