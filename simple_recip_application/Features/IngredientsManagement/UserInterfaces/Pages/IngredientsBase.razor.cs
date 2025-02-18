using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using simple_recip_application.Features.IngredientsManagement.Store;
using simple_recip_application.Features.IngredientsManagement.Store.Actions;
using simple_recip_application.Resources;

namespace simple_recip_application.Features.IngredientsManagement.Pages;

public class IngredientsBase : Fluxor.Blazor.Web.Components.FluxorComponent
{
    [Inject] protected IDispatcher Dispatcher { get; set; } = default!;
    [Inject] protected IState<IngredientState> IngredientState { get; set; } = default!;
    [Inject] protected IStringLocalizer<Labels> LabelsLocalizer { get; set; } = default!;
    [Inject] protected IStringLocalizer<Messages> MessagesLocalizer { get; set; } = default!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

    protected void LoadIngredients()
    {
        Dispatcher.Dispatch(new LoadIngredientsAction());
    }

    protected void DeleteIngredient(Guid id)
    {
        Dispatcher.Dispatch(new DeleteIngredientAction(id));
    }
    
    protected void UpdateIngredient(Guid id)
    {
        var uri = string.Concat("/edit-ingredient/", id);
        
        NavigationManager.NavigateTo(uri);
    }

    protected string GetLoadingVisibilityCssClass() => IngredientState.Value.IsLoading ? "" : "hidden";
    protected string GetErrorVisibilityCssClass() => !string.IsNullOrEmpty(IngredientState.Value.ErrorMessage) ? "" : "hidden";
    protected string GetIngredientsVisibilityCssClass() => (!IngredientState.Value.IsLoading && string.IsNullOrEmpty(IngredientState.Value.ErrorMessage)) ? "" : "hidden";
}