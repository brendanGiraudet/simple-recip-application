using Fluxor;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.EqualityComparers;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.HouseholdProductsManagement.Store;

public static class HouseholdProductReducer
{
    #region LoadHouseholdProducts
    [ReducerMethod]
    public static HouseholdProductState ReduceLoadItemsAction(HouseholdProductState state, LoadItemsAction<IHouseholdProductModel> action) => state with { IsLoading = true, Take = action.Take, Skip = action.Skip };

    [ReducerMethod]
    public static HouseholdProductState ReduceLoadItemsSuccessAction(HouseholdProductState state, LoadItemsSuccessAction<IHouseholdProductModel> action) =>
        state with { Items = action.Items, IsLoading = false };

    [ReducerMethod]
    public static HouseholdProductState ReduceLoadItemsFailureAction(HouseholdProductState state, LoadItemsFailureAction<IHouseholdProductModel> action) =>
        state with { IsLoading = false };
    #endregion

    #region AddHouseholdProduct
    [ReducerMethod]
    public static HouseholdProductState ReduceAddItemAction(HouseholdProductState state, AddItemAction<IHouseholdProductModel> action) =>
        state with { Items = state.Items, IsLoading = true };

    [ReducerMethod]
    public static HouseholdProductState ReduceAddItemSuccessAction(HouseholdProductState state, AddItemSuccessAction<IHouseholdProductModel> action) =>
        state with { Items = [.. state.Items, action.Item], IsLoading = false };

    [ReducerMethod]
    public static HouseholdProductState ReduceAddItemFailureAction(HouseholdProductState state, AddItemFailureAction<IHouseholdProductModel> action) =>
        state with { Items = state.Items, IsLoading = false };
    #endregion

    #region DeleteHouseholdProduct
    [ReducerMethod]
    public static HouseholdProductState ReduceDeleteItemAction(HouseholdProductState state, DeleteItemAction<IHouseholdProductModel> action) => state with { IsLoading = true };

    [ReducerMethod]
    public static HouseholdProductState ReduceDeleteItemSuccessAction(HouseholdProductState state, DeleteItemSuccessAction<IHouseholdProductModel> action)
    {
        var ingredients = state.Items.Where(i => !new HouseholdProductEqualityComparer().Equals(i, action.Item)).ToList();

        return state with { Items = ingredients, IsLoading = false };
    }

    [ReducerMethod]
    public static HouseholdProductState ReduceDeleteItemFailureAction(HouseholdProductState state, DeleteItemFailureAction<IHouseholdProductModel> action) => state with { IsLoading = false };
    #endregion

    #region UpdateHouseholdProduct
    [ReducerMethod]
    public static HouseholdProductState ReduceUpdateItemAction(HouseholdProductState state, UpdateItemAction<IHouseholdProductModel> action) =>
        state with { IsLoading = true };

    [ReducerMethod]
    public static HouseholdProductState ReduceUpdateItemSuccessAction(HouseholdProductState state, UpdateItemSuccessAction<IHouseholdProductModel> action)
    {
        var updatedProducts = state.Items.Select(i => i.Id == action.Item.Id ? action.Item : i).ToList();
        return state with { Items = updatedProducts, IsLoading = false };
    }

    [ReducerMethod]
    public static HouseholdProductState ReduceUpdateItemFailureAction(HouseholdProductState state, UpdateItemFailureAction<IHouseholdProductModel> action) =>
        state with { IsLoading = false };
    #endregion

    #region SetHouseholdProductModalVisibility
    [ReducerMethod]
    public static HouseholdProductState ReduceSetHouseholdProductModalVisibilityAction(HouseholdProductState state, SetFormModalVisibilityAction<IHouseholdProductModel> action) =>
        state with { FormModalVisibility = action.IsVisible };
    #endregion
}
