using System.Linq.Expressions;

namespace GymApp.Shared.Specification;

public abstract class Specification<T>
{
    public abstract Expression<Func<T, bool>>? ToExpression();

    public bool IsSatisfiedBy(T entity)
    {
        Func<T, bool> predicate = ToExpression()!.Compile();
        return predicate(entity);
    }
}