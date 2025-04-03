using System.Diagnostics.CodeAnalysis;
using simple_recip_application.Features.ProductsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.ProductsManagement.ApplicationCore.EqualityComparers;

public class ProductEqualityComparer : IEqualityComparer<IProductModel>
{
    public bool Equals(IProductModel? x, IProductModel? y)
    {
        if(x is null || y is null) return false;

        return string.Equals(x.Name, y.Name) &&
               Guid.Equals(x.Id, y.Id);
    }

    public int GetHashCode([DisallowNull] IProductModel obj)
    {
        return obj.GetHashCode();
    }
}