using System.Linq.Expressions;

namespace GymApp.Shared.Specification;

public class AndSpecification<T>(ISpecification<T> left, ISpecification<T> right) : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>>? leftExpression = left.ToExpression();
        Expression<Func<T, bool>>? rightExpression = right.ToExpression();

        var parameter = Expression.Parameter(typeof(T), leftExpression!.Parameters.Single().Name ?? "x");

        var leftBody = new ParameterReplacer(leftExpression.Parameters.Single(), parameter).Visit(leftExpression.Body);
        var rightBody = new ParameterReplacer(rightExpression!.Parameters.Single(), parameter).Visit(rightExpression.Body);

        BinaryExpression andExpression = Expression.AndAlso(leftBody!, rightBody!);

        return Expression.Lambda<Func<T, bool>>(andExpression, parameter);
    }
}