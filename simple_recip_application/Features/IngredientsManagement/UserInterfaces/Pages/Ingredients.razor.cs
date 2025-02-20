using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;
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
    protected IIngredientModel? SelectedIngredient { get; set; } = new IngredientModel();

    protected void OpenAddIngredientModal()
    {
        SelectedIngredient = new IngredientModel();
        IsIngredientModalOpen = true;
    }

    protected void OpenEditIngredientModal(IIngredientModel model)
    {
        SelectedIngredient = model;
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

    protected void DeleteIngredient(IIngredientModel model)
    {
        if (model.Id.HasValue)
            Dispatcher.Dispatch(new DeleteIngredientAction(model.Id.Value));
    }

    protected string GetIngredientsVisibilityCssClass() => (!IngredientState.Value.IsLoading && string.IsNullOrEmpty(IngredientState.Value.ErrorMessage)) ? "" : "hidden";

    protected void HandleSelection(IIngredientModel ingredient)
    {
        if (IngredientState.Value.SelectedIngredients.Contains(ingredient))
            IngredientState.Value.SelectedIngredients.Remove(ingredient);

        else
            IngredientState.Value.SelectedIngredients.Add(ingredient);
    }
}