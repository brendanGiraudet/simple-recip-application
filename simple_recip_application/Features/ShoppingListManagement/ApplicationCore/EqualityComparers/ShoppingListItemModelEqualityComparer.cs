using System.Diagnostics.CodeAnalysis;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.ShoppingListManagement.ApplicationCore.EqualityComparers;

public class ShoppingListItemModelEqualityComparer : IEqualityComparer<IShoppingListItemModel>
{
    public bool Equals(IShoppingListItemModel? x, IShoppingListItemModel? y)
    {
        if (x is null || y is null) return false;

        return string.Equals(x.UserId, y.UserId) &&
               decimal.Equals(x.Quantity, y.Quantity) &&
               Guid.Equals(x.ProductId, y.ProductId) &&
               bool.Equals(x.IsDone, y.IsDone);
    }

    public int GetHashCode([DisallowNull] IShoppingListItemModel obj)
    {
        return obj.GetHashCode();
    }
}