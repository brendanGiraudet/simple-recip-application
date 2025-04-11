using System.Linq.Expressions;
using Fluxor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using simple_recip_application.Components.OptionsMenu;
using simple_recip_application.Constants;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.IngredientsManagement.Store;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.IngredientsManagement.UserInterfaces.Pages.Ingredients;

public partial class Ingredients
{
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IState<IngredientState> IngredientState { get; set; }
    [Inject] public required IIngredientFactory IngredientFactory { get; set; }
    [Inject] public required IAuthorizationService AuthorizationService { get; set; }
    [Inject] public required AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    private IIngredientModel? _selectedIngredient { get; set; }

    private bool _canManageIngredient;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

        var authorizationResult = await AuthorizationService.AuthorizeAsync(authenticationState.User, FeatureFlagsConstants.IngredientManagementFeature);

        _canManageIngredient = authorizationResult.Succeeded;
    }

    private async Task OpenIngredientFormModalAsync(IIngredientModel? model = null)
    {
        if (!_canManageIngredient)
            return;

        _selectedIngredient = model ?? IngredientFactory.CreateIngredient();

        Dispatcher.Dispatch(new SetFormModalVisibilityAction<IIngredientModel>(true));

        await Task.CompletedTask;
    }

    private void CloseIngredientModal(bool isUpdated) => Dispatcher.Dispatch(new SetFormModalVisibilityAction<IIngredientModel>(false));

    protected override void OnInitialized()
    {
        base.OnInitialized();

        LoadFilteredIngredients();

        _selectedIngredient = IngredientFactory.CreateIngredient();
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
        List<OptionMenuItem> options = [];

        if (_canManageIngredient)
            options.Add(new(MaterialIconsConstants.Add, string.Empty, () => OpenIngredientFormModalAsync(), LabelsTranslator.AddIngredient));

        return options;
    }

    private bool CanPreviousClick() => IngredientState.Value.Skip > 0;
    private async Task OnPrevious()
    {
        if (!CanPreviousClick()) return;

        var skip = IngredientState.Value.Skip - IngredientState.Value.Take;

        skip = skip < 0 ? 0 : skip;

        LoadFilteredIngredients(skip);

        await Task.CompletedTask;
    }

    private async Task OnNext()
    {
        var skip = IngredientState.Value.Skip + IngredientState.Value.Take;

        LoadFilteredIngredients(skip);

        await Task.CompletedTask;
    }

    private string _searchTerm = string.Empty;

    private void OnSearchTermChanged(string searchTerm)
    {
        _searchTerm = searchTerm;
        LoadFilteredIngredients();
    }

    private void LoadFilteredIngredients(int? skip = null)
    {
        Expression<Func<IIngredientModel, bool>>? filter = c => c.RemoveDate == null;

        if (!string.IsNullOrEmpty(_searchTerm))
            filter = i => i.Name.ToLower().Contains(_searchTerm.ToLower()) && i.RemoveDate == null;

        Dispatcher.Dispatch(new LoadItemsAction<IIngredientModel>(Take: IngredientState.Value.Take, Skip: skip ?? 0, filter));
    }
}