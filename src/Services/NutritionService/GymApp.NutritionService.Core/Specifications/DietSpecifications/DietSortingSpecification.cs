using GymApp.NutritionService.Data.Entities;

namespace GymApp.NutritionService.Core.Specifications.DietSpecifications;

public class DietSortingSpecification : PagingSpecification<Diet>
{
    public DietSortingSpecification(DietSpecificationParameters paginationParams) : base(paginationParams)
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