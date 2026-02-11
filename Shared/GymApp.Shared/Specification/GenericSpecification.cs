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

    public IQueryable<T> ApplyWhereCriteria(IQueryable<T> query)
    {
        if (Criteria != null)
        {
            query = query.Where(Criteria);
        }

        return query;
    }

    public Expression<Func<T, object>>? OrderBy { get; set; }
    public Expression<Func<T, object>>? OrderByDesc { get; set; }

    protected void AddOrderBy(Expression<Func<T, object>> orderBy)
    {
        OrderBy = orderBy;
    }

    protected void AddOrderByDesc(Expression<Func<T, object>> orderByDesc)
    {
        OrderByDesc = orderByDesc;
    }

    public bool IsPagingEnabled { get; set; }
    public int Skip { get; protected set; }
    public int Take { get; protected set; }

    protected void AddPaging(int skip, int take)
    {
        IsPagingEnabled = true;
        Skip = skip;
        Take = take;
    }
}