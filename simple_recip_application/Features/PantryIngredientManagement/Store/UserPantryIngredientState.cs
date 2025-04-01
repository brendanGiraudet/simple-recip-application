using Fluxor;
using simple_recip_application.Features.PantryIngredientManagement.ApplicationCore.Entities;
using simple_recip_application.Store;

namespace simple_recip_application.Features.PantryIngredientManagement.Store;

[FeatureState]
public record UserPantryIngredientState : BaseState<IUserPantryIngredientModel>
{
    public string UserId { get; init; } = string.Empty;
}
