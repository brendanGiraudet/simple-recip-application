using Fluxor;
using simple_recip_application.Features.RecipesManagement.ApplicationCore;
using simple_recip_application.Store;

namespace simple_recip_application.Features.RecipesManagement.Store;

[FeatureState]
public record class RecipeState : BaseState<IRecipeModel>
{
    public bool RecipeFormModalVisibility { get; set; } = false;
}
