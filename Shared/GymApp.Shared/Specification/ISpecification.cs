using System.Linq.Expressions;

namespace GymApp.Shared.Specification;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    IQueryable<T> ApplyWhereCriteria(IQueryable<T> query);
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDesc { get; }
    bool IsPagingEnabled { get; }
    int Skip { get; }
    int Take { get; }
}