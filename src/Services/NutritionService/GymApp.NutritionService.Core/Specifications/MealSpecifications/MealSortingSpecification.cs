using GymApp.NutritionService.Data.Entities;

namespace GymApp.NutritionService.Core.Specifications.MealSpecifications;

public class MealSortingSpecification : PagingSpecification<Meal>
{
    public MealSortingSpecification(MealSpecificationParameters paginationParams) : base(paginationParams)
    {
        switch (paginationParams.Sort)
        {
            case "calories":
                AddOrderBy(m => m.Calories);
                break;
            case "protein":
                AddOrderBy(m => m.Protein);
                break;
            default:
                AddOrderBy(m => m.Name!);
                break;
        }
    }
}