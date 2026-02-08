using System.Linq.Expressions;

namespace GymApp.Shared.Specification;

public class GenericSpecification<T> : ISpecification<T>
{
    public GenericSpecification()
    {

    }

    public GenericSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>>? Criteria { get; set; }
    public Expression<Func<T, object>>? OrderBy { get; set; }
    public Expression<Func<T, object>>? OrderByDesc { get; set; }

    public void AddOrderBy(Expression<Func<T, object>> orderBy)
    {
        OrderBy = orderBy;
    }

    public void AddOrderByDesc(Expression<Func<T, object>> orderByDesc)
    {
        OrderByDesc = orderByDesc;
    }
}