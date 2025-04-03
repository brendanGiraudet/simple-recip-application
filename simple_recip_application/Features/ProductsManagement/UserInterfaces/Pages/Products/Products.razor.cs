using Fluxor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using simple_recip_application.Components.OptionsMenu;
using simple_recip_application.Constants;
using simple_recip_application.Features.ProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.ProductsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.ProductsManagement.Store;
using simple_recip_application.Features.ProductsManagement.Store.Actions;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;
using System.Linq.Expressions;

namespace simple_recip_application.Features.ProductsManagement.UserInterfaces.Pages.Products;

public partial class Products
{
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IState<ProductState> ProductState { get; set; }
    [Inject] public required IProductFactory ProductFactory { get; set; }
    [Inject] public required IAuthorizationService AuthorizationService { get; set; }
    [Inject] public required AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    private IProductModel? _selectedProduct { get; set; }

    private bool _canManageProduct;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

        var authorizationResult = await AuthorizationService.AuthorizeAsync(authenticationState.User, FeatureFlagsConstants.ProductManagementFeature);

        _canManageProduct = authorizationResult.Succeeded;
    }

    private async Task OpenProductFormModalAsync(IProductModel? model = null)
    {
        if (!_canManageProduct)
            return;

        _selectedProduct = model ?? ProductFactory.CreateProduct();

        Dispatcher.Dispatch(new SetProductModalVisibilityAction(true));

        await Task.CompletedTask;
    }

    private void CloseProductModal(bool isUpdated) => Dispatcher.Dispatch(new SetProductModalVisibilityAction(false));

    protected override void OnInitialized()
    {
        base.OnInitialized();

        LoadFilteredProducts();

        _selectedProduct = ProductFactory.CreateProduct();
    }

    private string GetProductsVisibilityCssClass() => !ProductState.Value.IsLoading ? "" : "hidden";

    private void HandleSelection(IProductModel ingredient)
    {
        if (ProductState.Value.SelectedItems.Contains(ingredient))
            ProductState.Value.SelectedItems = ProductState.Value.SelectedItems.Except([ingredient]);

        else
            ProductState.Value.SelectedItems = ProductState.Value.SelectedItems.Append(ingredient);
    }

    private List<OptionMenuItem> GetOptions()
    {
        List<OptionMenuItem> options = [];

        if(_canManageProduct)
            options.Add(new ("add", string.Empty, () => OpenProductFormModalAsync(), LabelsTranslator.AddProduct));

        return options;
    }

    private bool CanPreviousClick() => ProductState.Value.Skip > 0;
    private async Task OnPrevious()
    {
        if (!CanPreviousClick()) return;

        var skip = ProductState.Value.Skip - ProductState.Value.Take;

        skip = skip < 0 ? 0 : skip;

        LoadFilteredProducts(skip);

        await Task.CompletedTask;
    }

    private async Task OnNext()
    {
        var skip = ProductState.Value.Skip + ProductState.Value.Take;

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
        Expression<Func<IProductModel, bool>>? filter = c => c.RemoveDate == null;

        if (!string.IsNullOrEmpty(_searchTerm))
            filter = i => i.Name.ToLower().Contains(_searchTerm.ToLower()) && i.RemoveDate == null;

        Dispatcher.Dispatch(new LoadItemsAction<IProductModel>(Take: ProductState.Value.Take, Skip: skip ?? 0, filter));
    }
}