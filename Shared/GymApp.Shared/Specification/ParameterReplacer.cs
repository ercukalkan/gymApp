using System.Linq.Expressions;

namespace GymApp.Shared.Specification;

internal sealed class ParameterReplacer(ParameterExpression source, ParameterExpression target) : ExpressionVisitor
{
    protected override Expression VisitParameter(ParameterExpression node)
    {
        return node == source ? target : base.VisitParameter(node);
    }
}