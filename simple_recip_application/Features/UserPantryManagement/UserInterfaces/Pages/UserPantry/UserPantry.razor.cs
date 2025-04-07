using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.EqualityComparers;
using simple_recip_application.Features.UserInfos.Store;
using simple_recip_application.Features.UserPantryManagement.ApplicationCore.Entities;
using simple_recip_application.Features.UserPantryManagement.ApplicationCore.Factories;
using simple_recip_application.Features.UserPantryManagement.Store;
using simple_recip_application.Features.UserPantryManagement.Store.Actions;

namespace simple_recip_application.Features.UserPantryManagement.UserInterfaces.Pages.UserPantry;

public partial class UserPantry
{
    [Inject] public required IState<UserPantryState> UserPantryState { get; set; }
    [Inject] public required IState<UserInfosState> UserInfosState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IUserPantryItemFactory UserPantryItemFactory { get; set; }

    private async Task SearchProducts(string? searchTerm = null)
    {
        Dispatcher.Dispatch(new SearchProductsAction(searchTerm));

        await Task.CompletedTask;
    }

    private async Task AddUserPantryItemModel(IProductModel productModel)
    {
        if (!UserPantryState.Value.Items.Any(i => new ProductEqualityComparer().Equals(i.ProductModel, productModel)))
        {
            var userPantryItem = UserPantryItemFactory.Create(UserInfosState.Value.UserInfo.Id, productModel, 1);

            Dispatcher.Dispatch(new AddOrUpdateUserPantryItemAction(userPantryItem));
        }

        await Task.CompletedTask;
    }

    private void RemoveUserPantryItemModel(IUserPantryItemModel userPantryItem)
    {
        Dispatcher.Dispatch(new DeleteUserPantryItemAction(userPantryItem));
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (UserPantryState.Value.FilteredProducts.Count() == 0)
        {
            await SearchProducts();
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (UserInfosState.Value.UserInfo is not null)
            Dispatcher.Dispatch(new LoadUserPantryItemsAction(UserInfosState.Value.UserInfo?.Id));
    }

    private void UpdateUserPantryItem(IUserPantryItemModel userPantryItemModel, decimal quantity)
    {
        userPantryItemModel.Quantity = quantity;

        Dispatcher.Dispatch(new AddOrUpdateUserPantryItemAction(userPantryItemModel));
    }
}
