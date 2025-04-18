using System.Diagnostics.CodeAnalysis;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.IngredientsManagement.ApplicationCore.EqualityComparers;

public class IngredientEqualityComparer : IEqualityComparer<IIngredientModel>
{
    public bool Equals(IIngredientModel? x, IIngredientModel? y)
    {
        if(x is null || y is null) return false;

        return string.Equals(x.Name, y.Name) &&
               Guid.Equals(x.Id, y.Id);
    }

    public int GetHashCode([DisallowNull] IIngredientModel obj)
    {
        return obj.GetHashCode();
    }
}