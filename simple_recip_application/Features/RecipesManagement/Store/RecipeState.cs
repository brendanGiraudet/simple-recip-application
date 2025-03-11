using Fluxor;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;
using simple_recip_application.Store;

namespace simple_recip_application.Features.RecipesManagement.Store;

[FeatureState]
public record class RecipeState : BaseState<IRecipeModel>
{
    public bool RecipeFormModalVisibility { get; set; } = false;
    public IEnumerable<IIngredientModel> FilteredIngredients { get; set; } = [];

    private RecipeState()
    {
        Item = new RecipeModel();
    }
}
