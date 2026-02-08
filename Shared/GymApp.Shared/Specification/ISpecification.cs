using System.Linq.Expressions;

namespace GymApp.Shared.Specification;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
}