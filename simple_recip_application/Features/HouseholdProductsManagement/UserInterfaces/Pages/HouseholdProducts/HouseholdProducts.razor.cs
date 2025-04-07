using Fluxor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using simple_recip_application.Components.OptionsMenu;
using simple_recip_application.Constants;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.HouseholdProductsManagement.Store;
using simple_recip_application.Features.HouseholdProductsManagement.Store.Actions;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;
using System.Linq.Expressions;

namespace simple_recip_application.Features.HouseholdProductsManagement.UserInterfaces.Pages.HouseholdProducts;

public partial class HouseholdProducts
{
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IState<HouseholdProductState> HouseholdProductState { get; set; }
    [Inject] public required IHouseholdProductFactory HouseholdProductFactory { get; set; }
    [Inject] public required IAuthorizationService AuthorizationService { get; set; }
    [Inject] public required AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    private IHouseholdProductModel? _selectedProduct { get; set; }

    private bool _canManageProduct;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

        var authorizationResult = await AuthorizationService.AuthorizeAsync(authenticationState.User, FeatureFlagsConstants.ProductManagementFeature);

        _canManageProduct = authorizationResult.Succeeded;
    }

    private async Task OpenProductFormModalAsync(IHouseholdProductModel? model = null)
    {
        if (!_canManageProduct)
            return;

        _selectedProduct = model ?? HouseholdProductFactory.CreateHouseholdProduct();

        Dispatcher.Dispatch(new SetHouseholdProductModalVisibilityAction(true));

        await Task.CompletedTask;
    }

    private void CloseProductModal(bool isUpdated) => Dispatcher.Dispatch(new SetHouseholdProductModalVisibilityAction(false));

    protected override void OnInitialized()
    {
        base.OnInitialized();

        LoadFilteredProducts();

        _selectedProduct = HouseholdProductFactory.CreateHouseholdProduct();
    }

    private string GetProductsVisibilityCssClass() => !HouseholdProductState.Value.IsLoading ? "" : "hidden";

    private void HandleSelection(IHouseholdProductModel ingredient)
    {
        if (HouseholdProductState.Value.SelectedItems.Contains(ingredient))
            HouseholdProductState.Value.SelectedItems = HouseholdProductState.Value.SelectedItems.Except([ingredient]);

        else
            HouseholdProductState.Value.SelectedItems = HouseholdProductState.Value.SelectedItems.Append(ingredient);
    }

    private List<OptionMenuItem> GetOptions()
    {
        List<OptionMenuItem> options = [];

        if(_canManageProduct)
            options.Add(new ("add", string.Empty, () => OpenProductFormModalAsync(), LabelsTranslator.AddProduct));

        return options;
    }

    private bool CanPreviousClick() => HouseholdProductState.Value.Skip > 0;
    private async Task OnPrevious()
    {
        if (!CanPreviousClick()) return;

        var skip = HouseholdProductState.Value.Skip - HouseholdProductState.Value.Take;

        skip = skip < 0 ? 0 : skip;

        LoadFilteredProducts(skip);

        await Task.CompletedTask;
    }

    private async Task OnNext()
    {
        var skip = HouseholdProductState.Value.Skip + HouseholdProductState.Value.Take;

        LoadFilteredProducts(skip);

        await Task.CompletedTask;
    }

    private string _searchTerm = string.Empty;

    private void OnSearchTermChanged(string searchTerm)
    {
        _searchTerm = searchTerm;
        LoadFilteredProducts();
    }

    private void LoadFilteredProducts(int? skip = null)
    {
        Expression<Func<IHouseholdProductModel, bool>>? filter = c => c.RemoveDate == null;

        if (!string.IsNullOrEmpty(_searchTerm))
            filter = i => i.Name.ToLower().Contains(_searchTerm.ToLower()) && i.RemoveDate == null;

        Dispatcher.Dispatch(new LoadItemsAction<IHouseholdProductModel>(Take: HouseholdProductState.Value.Take, Skip: skip ?? 0, filter));
    }
}