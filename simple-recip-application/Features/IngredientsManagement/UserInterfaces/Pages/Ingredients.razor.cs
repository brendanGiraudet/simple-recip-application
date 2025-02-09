using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Features.IngredientsManagement.Store;
using simple_recip_application.Features.IngredientsManagement.Store.Actions;

namespace simple_recip_application.Features.IngredientsManagement.Pages;

public class IngredientsBase : Fluxor.Blazor.Web.Components.FluxorComponent
{
    [Inject] protected IDispatcher Dispatcher { get; set; } = default!;
    [Inject] protected IState<IngredientState> IngredientState { get; set; } = default!;

    protected void LoadIngredients()
    {
        Dispatcher.Dispatch(new LoadIngredientsAction());
    }

    protected string GetLoadingVisibilityCssClass() => IngredientState.Value.IsLoading ? "" : "hidden";
    protected string GetErrorVisibilityCssClass() => !string.IsNullOrEmpty(IngredientState.Value.ErrorMessage) ? "" : "hidden";
    protected string GetIngredientsVisibilityCssClass() => (!IngredientState.Value.IsLoading && string.IsNullOrEmpty(IngredientState.Value.ErrorMessage)) ? "" : "hidden";
}