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
}