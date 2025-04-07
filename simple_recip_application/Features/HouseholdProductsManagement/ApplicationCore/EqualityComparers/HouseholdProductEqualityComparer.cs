using System.Diagnostics.CodeAnalysis;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.EqualityComparers;

public class HouseholdProductEqualityComparer : IEqualityComparer<IHouseholdProductModel>
{
    public bool Equals(IHouseholdProductModel? x, IHouseholdProductModel? y)
    {
        if(x is null || y is null) return false;

        return string.Equals(x.Name, y.Name) &&
               Guid.Equals(x.Id, y.Id);
    }

    public int GetHashCode([DisallowNull] IHouseholdProductModel obj)
    {
        return obj.GetHashCode();
    }
}