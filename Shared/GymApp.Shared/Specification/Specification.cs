using System.Linq.Expressions;

namespace GymApp.Shared.Specification;

public class Specification<T>(Expression<Func<T, bool>> expression)
{
    public Expression<Func<T, bool>>? Expression => expression;

    public bool IsSatisfiedBy(T entity)
    {
        return Expression!.Compile().Invoke(entity);
    }
}