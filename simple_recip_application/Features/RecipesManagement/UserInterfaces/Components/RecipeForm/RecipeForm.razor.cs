using simple_recip_application.Features.RecipesManagement.ApplicationCore;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipesManagement.UserInterfaces.Components.RecipeForm;

public partial class RecipeForm
{
    private IRecipeModel Recipe { get; set; } = new RecipeModel();

    private void AddRecipe()
    {
        Dispatcher.Dispatch(new AddItemAction<IRecipeModel>(Recipe));
    }
}