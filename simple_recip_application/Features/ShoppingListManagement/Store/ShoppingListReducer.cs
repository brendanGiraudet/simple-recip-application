using Fluxor;
using simple_recip_application.Features.ShoppingList.Store.Actions;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.EqualityComparers;
using simple_recip_application.Features.ShoppingListManagement.Store.Actions;

namespace simple_recip_application.Features.ShoppingListManagement.Store;

public static class ShoppingListReducer
{
    #region Load
    [ReducerMethod]
    public static ShoppingListState OnLoad(ShoppingListState state, LoadShoppingListItemsAction action)
        => state with { IsLoading = true };

    [ReducerMethod]
    public static ShoppingListState OnLoadSuccess(ShoppingListState state, LoadShoppingListItemsSuccessAction action)
        => state with { Items = action.Items.OrderBy(c => c.ProductModel?.Name), IsLoading = false };

    [ReducerMethod]
    public static ShoppingListState OnLoadFailure(ShoppingListState state, LoadShoppingListItemsFailureAction action)
        => state with { IsLoading = false };
    #endregion

    #region SearchShoppingListProducts
    [ReducerMethod]
    public static ShoppingListState OnSearchProductsAction(ShoppingListState state, SearchShoppingListProductsAction action)
        => state with { IsLoadingFilteredProducts = true };

    [ReducerMethod]
    public static ShoppingListState OnSearchProductsSuccessAction(ShoppingListState state, SearchShoppingListProductsSuccessAction action)
        => state with { FilteredProducts = action.ProductModels.OrderBy(c => c.Name), IsLoadingFilteredProducts = false };

    [ReducerMethod]
    public static ShoppingListState OnSearchProductsFailureAction(ShoppingListState state, SearchShoppingListProductsFailureAction action)
        => state with { IsLoadingFilteredProducts = false };
    #endregion

    #region AddOrUpdate
    [ReducerMethod]
    public static ShoppingListState OnAddOrUpdateSuccess(ShoppingListState state, AddOrUpdateShoppingListItemSuccessAction action)
        => state with { 
            Items = state.Items
                         .Where(i => i.ProductId != action.Item.ProductId)
                         .Append(action.Item)
                         .OrderBy(c => c.ProductModel?.Name), 
            FilteredProducts = state.FilteredProducts
                                    .Where(i => i.Id != action.Item.ProductId)
                                    .OrderBy(c => c.Name) 
        };
    #endregion

    #region Delete
    [ReducerMethod]
    public static ShoppingListState OnDeleteSuccess(ShoppingListState state, DeleteShoppingListItemSuccessAction action)
        => state with { 
            Items = state.Items
                         .Where(i => !new ShoppingListItemModelEqualityComparer().Equals(i, action.Item))
                         .OrderBy(c => c.ProductModel?.Name),
            FilteredProducts = state.FilteredProducts.Append(action.Item.ProductModel)
                                                     .OrderBy(c => c.Name)
        };
    #endregion
}