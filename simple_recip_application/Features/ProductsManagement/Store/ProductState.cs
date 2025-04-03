using Fluxor;
using simple_recip_application.Features.ProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Store;

namespace simple_recip_application.Features.ProductsManagement.Store;

[FeatureState]
public record class ProductState : BaseState<IProductModel>
{
    public bool ProductModalVisibility { get; set; } = false;
}