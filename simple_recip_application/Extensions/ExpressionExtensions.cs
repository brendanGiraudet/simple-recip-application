using System.Linq.Expressions;

namespace simple_recip_application.Extensions;

public static class ExpressionExtensions
{
    public static Expression<Func<TTarget, bool>> Convert<TSource, TTarget>(this Expression<Func<TSource, bool>> source)
    {
        var parameter = Expression.Parameter(typeof(TTarget), "x");

        // var body = new ExpressionVisitor().Visit(source.Body); // Vous devez visiter l'arbre d'expressions pour modifier le type de source à target.
        
        return Expression.Lambda<Func<TTarget, bool>>(source.Body, parameter);
    }
}
