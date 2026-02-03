using System.Linq.Expressions;
using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Specification;

namespace GymApp.NutritionService.Core.Specifications.FoodSpecifications;

public class StartsWithSpecification(string startsWithChar) : Specification<Food>
{
    public override Expression<Func<Food, bool>>? ToExpression()
    {
        return food =>
            string.IsNullOrEmpty(startsWithChar) || food.Name!.StartsWith(startsWithChar);
    }
}