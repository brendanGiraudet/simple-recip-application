using Fluxor;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Store;

namespace simple_recip_application.Features.HouseholdProductsManagement.Store;

[FeatureState]
public record class HouseholdProductState : BaseState<IHouseholdProductModel>
{
    public bool HouseholdProductModalVisibility { get; set; } = false;
}