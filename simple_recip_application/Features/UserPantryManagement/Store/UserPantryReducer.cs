using Fluxor;
using simple_recip_application.Features.UserPantryManagement.ApplicationCore.EqualityComparers;
using simple_recip_application.Features.UserPantryManagement.Store.Actions;

namespace simple_recip_application.Features.UserPantryManagement.Store;

public static class UserPantryReducer
{
    #region Load
    [ReducerMethod]
    public static UserPantryState OnLoad(UserPantryState state, LoadUserPantryItemsAction action)
        => state with { IsLoading = true };

    [ReducerMethod]
    public static UserPantryState OnLoadSuccess(UserPantryState state, LoadUserPantryItemsSuccessAction action)
        => state with { Items = action.Items.OrderBy(c => c.ProductModel?.Name), IsLoading = false };

    [ReducerMethod]
    public static UserPantryState OnLoadFailure(UserPantryState state, LoadUserPantryItemsFailureAction action)
        => state with { IsLoading = false };
    #endregion

    #region SearchProducts
    [ReducerMethod]
    public static UserPantryState OnSearchProductsAction(UserPantryState state, SearchProductsAction action)
        => state with { IsLoadingFilteredProducts = true };

    [ReducerMethod]
    public static UserPantryState OnSearchProductsSuccessAction(UserPantryState state, SearchProductsSuccessAction action)
        => state with { FilteredProducts = action.ProductModels.OrderBy(c => c.Name), IsLoadingFilteredProducts = false };

    [ReducerMethod]
    public static UserPantryState OnSearchProductsFailureAction(UserPantryState state, SearchProductsFailureAction action)
        => state with { IsLoadingFilteredProducts = false };
    #endregion

    #region AddOrUpdate
    [ReducerMethod]
    public static UserPantryState OnAddOrUpdateSuccess(UserPantryState state, AddOrUpdateUserPantryItemSuccessAction action)
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
    public static UserPantryState OnDeleteSuccess(UserPantryState state, DeleteUserPantryItemSuccessAction action)
        => state with { 
            Items = state.Items
                         .Where(i => !new UserPantryItemEqualityComparer().Equals(i, action.Item))
                         .OrderBy(c => c.ProductModel?.Name),
            FilteredProducts = state.FilteredProducts.Append(action.Item.ProductModel)
                                                     .OrderBy(c => c.Name)
        };
    #endregion
}