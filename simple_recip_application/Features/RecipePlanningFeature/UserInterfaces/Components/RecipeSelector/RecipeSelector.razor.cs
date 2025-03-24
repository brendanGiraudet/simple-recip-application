using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.Store;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipePlanningFeature.UserInterfaces.Components.RecipeSelector;

public partial class RecipeSelector
{
    [Inject] public required IState<RecipeState> RecipeState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }

    [Parameter] public EventCallback<IRecipeModel> OnRecipeSelected { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        Dispatcher.Dispatch(new LoadItemsAction<IRecipeModel>());
    }
}
