using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using simple_recip_application.Features.IngredientsManagement.Store;
using simple_recip_application.Features.IngredientsManagement.Store.Actions;
using simple_recip_application.Resources;

namespace simple_recip_application.Features.IngredientsManagement.UserInterfaces.Pages;

public partial class Ingredients
{
    [Inject] protected IDispatcher Dispatcher { get; set; } = default!;
    [Inject] protected IState<IngredientState> IngredientState { get; set; } = default!;
    [Inject] protected IStringLocalizer<Labels> LabelsLocalizer { get; set; } = default!;
    [Inject] protected IStringLocalizer<Messages> MessagesLocalizer { get; set; } = default!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

    protected bool IsIngredientModalOpen { get; set; } = false;
    protected Guid? SelectedIngredientId { get; set; }

    protected void OpenAddIngredientModal()
    {
        SelectedIngredientId = null;
        IsIngredientModalOpen = true;
    }

    protected void OpenEditIngredientModal(Guid id)
    {
        SelectedIngredientId = id;
        IsIngredientModalOpen = true;
    }

    protected void CloseIngredientModal(bool isUpdated)
    {
        IsIngredientModalOpen = false;
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        Dispatcher.Dispatch(new LoadIngredientsAction());
    }

    protected void DeleteIngredient(Guid id)
    {
        Dispatcher.Dispatch(new DeleteIngredientAction(id));
    }

    protected string GetIngredientsVisibilityCssClass() => (!IngredientState.Value.IsLoading && string.IsNullOrEmpty(IngredientState.Value.ErrorMessage)) ? "" : "hidden";
}