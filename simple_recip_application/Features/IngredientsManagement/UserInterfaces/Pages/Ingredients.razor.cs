using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using simple_recip_application.Components.OptionsMenu;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;
using simple_recip_application.Features.IngredientsManagement.Store;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;
using System.Linq.Expressions;

namespace simple_recip_application.Features.IngredientsManagement.UserInterfaces.Pages;

public partial class Ingredients
{
    [Inject] protected IDispatcher Dispatcher { get; set; } = default!;
    [Inject] protected IState<IngredientState> IngredientState { get; set; } = default!;
    [Inject] protected IStringLocalizer<Labels> LabelsLocalizer { get; set; } = default!;

    private bool _isIngredientModalOpen { get; set; } = false;
    private IIngredientModel? _selectedIngredient { get; set; } = new IngredientModel();

    private async Task OpenIngredientFormModalAsync(IIngredientModel? model = null)
    {
        _selectedIngredient = model ?? new IngredientModel();
        _isIngredientModalOpen = true;

        StateHasChanged();
    }

    private void CloseIngredientModal(bool isUpdated) => _isIngredientModalOpen = false;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Dispatcher.Dispatch(new LoadItemsAction<IIngredientModel>());
    }

    private void DeleteIngredient(IIngredientModel model)
    {
        if (model.Id.HasValue)
            Dispatcher.Dispatch(new DeleteItemAction<IIngredientModel>(model));
    }

    private string GetIngredientsVisibilityCssClass() => !IngredientState.Value.IsLoading ? "" : "hidden";

    private void HandleSelection(IIngredientModel ingredient)
    {
        if (IngredientState.Value.SelectedItems.Contains(ingredient))
            IngredientState.Value.SelectedItems = IngredientState.Value.SelectedItems.Except([ingredient]);

        else
            IngredientState.Value.SelectedItems = IngredientState.Value.SelectedItems.Append(ingredient);
    }

    private List<OptionMenuItem> GetOptions()
    {
        List<OptionMenuItem> options = [
            new ("add", LabelsLocalizer["AddIngredient"], () => OpenIngredientFormModalAsync())
        ];

        return options;
    }

    private bool CanPreviousClick() => IngredientState.Value.Skip > 0;
    private async Task OnPrevious()
    {
        if(!CanPreviousClick()) return;
        
        var skip = IngredientState.Value.Skip - IngredientState.Value.Take;

        skip = skip < 0 ? 0 : skip;

        Dispatcher.Dispatch(new LoadItemsAction<IIngredientModel>(Take: IngredientState.Value.Take, Skip: skip));
    }

    private async Task OnNext()
    {
        var skip = IngredientState.Value.Skip + IngredientState.Value.Take;
        Dispatcher.Dispatch(new LoadItemsAction<IIngredientModel>(Take: IngredientState.Value.Take, Skip: skip));
    }

    private string _searchTerm = string.Empty;

    private void OnSearchTermChanged(string searchTerm)
    {
        _searchTerm = searchTerm;
        LoadFilteredIngredients();
    }

    private void LoadFilteredIngredients()
    {
        Expression<Func<IIngredientModel, bool>>? filter = null;

        if(!string.IsNullOrEmpty(_searchTerm))
            filter = i => i.Name.ToLower().Contains(_searchTerm.ToLower());

        Dispatcher.Dispatch(new LoadItemsAction<IIngredientModel>(Take: IngredientState.Value.Take, Skip: 0, filter));
    }
}