using GymApp.NutritionService.Data.Entities;

namespace GymApp.NutritionService.Core.Specifications.DietSpecifications;

public class DietSortingSpecification : PagingSpecification<Diet>
{
    public DietSortingSpecification(DietSpecificationParameters paginationParams) : base(paginationParams)
    {
        switch (paginationParams.Sort)
        {
            case "calories":
                AddOrderBy(d => d.Calories);
                break;
            case "protein":
                AddOrderBy(d => d.Protein);
                break;
            default:
                AddOrderBy(d => d.Name!);
                break;
        }
    }
}