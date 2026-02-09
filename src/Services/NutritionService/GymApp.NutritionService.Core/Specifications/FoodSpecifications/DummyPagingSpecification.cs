using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Specification;

namespace GymApp.NutritionService.Core.Specifications.FoodSpecifications;

public class DummyPagingSpecification : GenericSpecification<Food>
{
    public DummyPagingSpecification(FoodSpecificationParameters specParams) : base()
    {
        AddPaging((specParams.PageNumber - 1) * specParams.PageSize, specParams.PageSize);

        switch (specParams.Sort)
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