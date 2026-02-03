using System.Linq.Expressions;

namespace GymApp.Shared.Specification;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? ToExpression();
    bool IsSatisfiedBy(T entity);
    Specification<T> And(ISpecification<T> specification);
    Specification<T> Or(ISpecification<T> specification);
}