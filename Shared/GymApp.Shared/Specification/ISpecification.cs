using System.Linq.Expressions;

namespace GymApp.Shared.Specification;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDesc { get; }
    int? Skip { get; }
    int? Take { get; }
}