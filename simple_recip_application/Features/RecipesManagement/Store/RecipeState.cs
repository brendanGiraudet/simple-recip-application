using Fluxor;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Entities;
using simple_recip_application.Store;

namespace simple_recip_application.Features.RecipesManagement.Store;

[FeatureState]
public record class RecipeState : EntityBaseState<IRecipeModel>
{
    public IEnumerable<IIngredientModel> FilteredIngredients { get; set; } = [];
    public IEnumerable<ITagModel> FilteredTags { get; set; } = [];

    private RecipeState()
    {
        Item = new RecipeModel();
    }
}
