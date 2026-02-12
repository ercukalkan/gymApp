using GymApp.NutritionService.Data.Entities;

namespace GymApp.NutritionService.Core.Specifications.FoodSpecifications;

public class FoodSortingSpecification : PagingSpecification<Food>
{
    public FoodSortingSpecification(FoodSpecificationParameters paginationParams) : base(paginationParams)
    {
        switch (paginationParams.Sort)
        {
            case "calories":
                AddOrderBy(f => f.Calories);
                break;
            case "protein":
                AddOrderBy(f => f.Protein);
                break;
            default:
                AddOrderBy(f => f.Name!);
                break;
        }
    }
}