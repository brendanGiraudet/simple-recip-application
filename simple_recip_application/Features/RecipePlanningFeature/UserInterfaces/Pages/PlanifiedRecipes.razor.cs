using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Features.RecipePlanningFeature.Store;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipePlanningFeature.UserInterfaces.Pages;

public partial class PlanifiedRecipes
{
    [Inject] public required IState<PlanifiedRecipeState> PlanifiedRecipeState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Dispatcher.Dispatch(new LoadItemsAction<IPlanifiedRecipeModel>());
    }
}