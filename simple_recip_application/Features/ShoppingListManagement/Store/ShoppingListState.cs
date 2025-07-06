using Fluxor;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Entities;
using simple_recip_application.Store;

namespace simple_recip_application.Features.ShoppingListManagement.Store;

[FeatureState]
public record ShoppingListState : BaseState<IShoppingListItemModel>
{
    public IEnumerable<IProductModel> FilteredProducts { get; set; } = [];
    public bool IsLoadingFilteredProducts { get; set; } = false;
}