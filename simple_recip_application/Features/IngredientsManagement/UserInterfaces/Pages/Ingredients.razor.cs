using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using simple_recip_application.Components.OptionsMenu;
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

    private bool _isIngredientModalOpen { get; set; } = false;
    private IIngredientModel? _selectedIngredient { get; set; } = new IngredientModel();

    private async Task OpenAddIngredientModalAsync()
    {
        _selectedIngredient = new IngredientModel();
        _isIngredientModalOpen = true;

        StateHasChanged();
    }

    private void OpenEditIngredientModal(IIngredientModel model)
    {
        _selectedIngredient = model;
        _isIngredientModalOpen = true;
    }

    private void CloseIngredientModal(bool isUpdated) => _isIngredientModalOpen = false;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Dispatcher.Dispatch(new LoadIngredientsAction());
    }

    private void DeleteIngredient(IIngredientModel model)
    {
        if (model.Id.HasValue)
            Dispatcher.Dispatch(new DeleteIngredientAction(model.Id.Value));
    }

    private string GetIngredientsVisibilityCssClass() => !IngredientState.Value.IsLoading ? "" : "hidden";

    private void HandleSelection(IIngredientModel ingredient)
    {
        if (IngredientState.Value.SelectedIngredients.Contains(ingredient))
            IngredientState.Value.SelectedIngredients.Remove(ingredient);

        else
            IngredientState.Value.SelectedIngredients.Add(ingredient);
    }

    private List<OptionMenuItem> GetOptions()
    {
        List<OptionMenuItem> options = [
            new ("add", LabelsLocalizer["AddIngredient"], () => OpenAddIngredientModalAsync())
        ];
        
        return options;
    }
}