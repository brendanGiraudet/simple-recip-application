using System.Linq.Expressions;

namespace simple_recip_application.Extensions;

public static class ExpressionExtensions
{
    public static Expression<Func<TTarget, bool>> Convert<TSource, TTarget>(this Expression<Func<TSource, bool>> source)
    {
        var parameter = Expression.Parameter(typeof(TTarget), "x");
        var visitor = new ParameterTypeVisitor<TSource, TTarget>(parameter);
        var body = visitor.Visit(source.Body);

        return Expression.Lambda<Func<TTarget, bool>>(body, parameter);
    }
}

public class ParameterTypeVisitor<TSource, TTarget> : ExpressionVisitor
{
    private readonly ParameterExpression _parameter;

    public ParameterTypeVisitor(ParameterExpression parameter)
    {
        _parameter = parameter;
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        if (node.Type == typeof(TSource))
        {
            return _parameter;
        }
        return base.VisitParameter(node);
    }
}
