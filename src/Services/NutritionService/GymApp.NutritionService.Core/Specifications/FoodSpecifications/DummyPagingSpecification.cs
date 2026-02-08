using GymApp.NutritionService.Data.Entities;
using GymApp.Shared.Specification;

namespace GymApp.NutritionService.Core.Specifications;

public class DummyPagingSpecification : GenericSpecification<Food>
{
    public DummyPagingSpecification(string? sort, int? pageNumber, int? pageSize) : base()
    {
        switch (sort)
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

        AddPaging((pageNumber - 1) * pageSize, pageSize);
    }
}