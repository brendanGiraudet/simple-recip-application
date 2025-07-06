using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.EqualityComparers;
using simple_recip_application.Features.UserInfos.Store;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Entities;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Factories;
using simple_recip_application.Features.ShoppingListManagement.Store;
using simple_recip_application.Features.ShoppingListManagement.Store.Actions;
using simple_recip_application.Features.ShoppingList.Store.Actions;

namespace simple_recip_application.Features.ShoppingListManagement.UserInterfaces.Pages.ShoppingList;

public partial class ShoppingList
{
    [Inject] public required IState<ShoppingListState> ShoppingListState { get; set; }
    [Inject] public required IState<UserInfosState> UserInfosState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IShoppingListItemModelFactory ShoppingListItemFactory { get; set; }

    private async Task SearchProducts(string? searchTerm = null)
    {
        Dispatcher.Dispatch(new SearchShoppingListProductsAction(searchTerm));

        await Task.CompletedTask;
    }

    private async Task AddShoppingListItemModel(IProductModel productModel)
    {
        if (!ShoppingListState.Value.Items.Any(i => new ProductEqualityComparer().Equals(i.ProductModel, productModel)))
        {
            var userPantryItem = ShoppingListItemFactory.Create(UserInfosState.Value.UserInfo.Id, productModel, 1, false);

            Dispatcher.Dispatch(new AddOrUpdateShoppingListItemAction(userPantryItem));
        }

        await Task.CompletedTask;
    }

    private void RemoveShoppingListItemModel(IShoppingListItemModel userPantryItem)
    {
        Dispatcher.Dispatch(new DeleteShoppingListItemAction(userPantryItem));
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (ShoppingListState.Value.FilteredProducts.Count() == 0)
        {
            await SearchProducts();
        }

        if (UserInfosState.Value.UserInfo is not null)
            Dispatcher.Dispatch(new LoadShoppingListItemsAction(UserInfosState.Value.UserInfo?.Id));
    }

    private void UpdateShoppingListItem(IShoppingListItemModel userPantryItemModel, decimal quantity)
    {
        userPantryItemModel.Quantity = quantity;

        Dispatcher.Dispatch(new AddOrUpdateShoppingListItemAction(userPantryItemModel));
    }
    
    private void UpdateShoppingListItem(IShoppingListItemModel userPantryItemModel, bool isDone)
    {
        userPantryItemModel.IsDone = isDone;

        Dispatcher.Dispatch(new AddOrUpdateShoppingListItemAction(userPantryItemModel));
    }
}
