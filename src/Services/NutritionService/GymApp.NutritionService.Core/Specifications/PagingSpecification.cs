using GymApp.Shared.Pagination;
using GymApp.Shared.Specification;

namespace GymApp.NutritionService.Core.Specifications;

public class PagingSpecification<T> : GenericSpecification<T>
{
    public PagingSpecification(PaginationParams paginationParams) : base()
    {
        AddPaging((paginationParams.PageNumber - 1) * paginationParams.PageSize, paginationParams.PageSize);
    }
}