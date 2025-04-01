using System.Diagnostics.CodeAnalysis;
using simple_recip_application.Features.PantryIngredientManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.PantryIngredientManagement.ApplicationCore.EqualityComparers;

public class UserPantryIngredientEqualityComparer : IEqualityComparer<IUserPantryIngredientModel>
{
    public bool Equals(IUserPantryIngredientModel? x, IUserPantryIngredientModel? y)
    {
        if (x is null || y is null) return false;

        return string.Equals(x.UserId, y.UserId) &&
               string.Equals(x.IngredientId, y.IngredientId) &&
               decimal.Equals(x.Quantity, y.Quantity);

    }

    public int GetHashCode([DisallowNull] IUserPantryIngredientModel obj)
    {
        return obj.GetHashCode();
    }
}
