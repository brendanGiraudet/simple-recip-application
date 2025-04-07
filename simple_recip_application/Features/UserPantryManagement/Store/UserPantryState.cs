using Fluxor;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.UserPantryManagement.ApplicationCore.Entities;
using simple_recip_application.Store;

namespace simple_recip_application.Features.UserPantryManagement.Store;

[FeatureState]
public record UserPantryState : BaseState<IUserPantryItemModel>
{
    public IEnumerable<IProductModel> FilteredProducts { get; set; } = [];
    public bool IsLoadingFilteredProducts { get; set; } = false;
}