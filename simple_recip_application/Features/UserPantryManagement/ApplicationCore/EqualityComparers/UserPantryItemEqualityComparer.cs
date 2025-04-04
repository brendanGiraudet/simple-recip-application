using System.Diagnostics.CodeAnalysis;
using simple_recip_application.Features.UserPantryManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.UserPantryManagement.ApplicationCore.EqualityComparers;

public class UserPantryItemEqualityComparer : IEqualityComparer<IUserPantryItemModel>
{
    public bool Equals(IUserPantryItemModel? x, IUserPantryItemModel? y)
    {
        if (x is null || y is null) return false;

        return string.Equals(x.UserId, y.UserId) &&
               decimal.Equals(x.Quantity, y.Quantity) &&
               Guid.Equals(x.ProductId, y.ProductId);
    }

    public int GetHashCode([DisallowNull] IUserPantryItemModel obj)
    {
        return obj.GetHashCode();
    }
}