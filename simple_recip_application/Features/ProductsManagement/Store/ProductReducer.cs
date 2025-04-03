using Fluxor;
using simple_recip_application.Features.ProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.ProductsManagement.ApplicationCore.EqualityComparers;
using simple_recip_application.Features.ProductsManagement.Store.Actions;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.ProductsManagement.Store;

public static class ProductReducer
{
    #region LoadProducts
    [ReducerMethod]
    public static ProductState ReduceLoadItemsAction(ProductState state, LoadItemsAction<IProductModel> action) => state with { IsLoading = true, Take = action.Take, Skip = action.Skip };

    [ReducerMethod]
    public static ProductState ReduceLoadItemsSuccessAction(ProductState state, LoadItemsSuccessAction<IProductModel> action) =>
        state with { Items = action.Items, IsLoading = false };

    [ReducerMethod]
    public static ProductState ReduceLoadItemsFailureAction(ProductState state, LoadItemsFailureAction<IProductModel> action) =>
        state with { IsLoading = false };
    #endregion

    #region AddProduct
    [ReducerMethod]
    public static ProductState ReduceAddItemAction(ProductState state, AddItemAction<IProductModel> action) =>
        state with { Items = state.Items, IsLoading = true };

    [ReducerMethod]
    public static ProductState ReduceAddItemSuccessAction(ProductState state, AddItemSuccessAction<IProductModel> action) =>
        state with { Items = [.. state.Items, action.Item], IsLoading = false };

    [ReducerMethod]
    public static ProductState ReduceAddItemFailureAction(ProductState state, AddItemFailureAction<IProductModel> action) =>
        state with { Items = state.Items, IsLoading = false };
    #endregion

    #region DeleteProduct
    [ReducerMethod]
    public static ProductState ReduceDeleteItemAction(ProductState state, DeleteItemAction<IProductModel> action) => state with { IsLoading = true };

    [ReducerMethod]
    public static ProductState ReduceDeleteItemSuccessAction(ProductState state, DeleteItemSuccessAction<IProductModel> action)
    {
        var ingredients = state.Items.Where(i => !new ProductEqualityComparer().Equals(i, action.Item)).ToList();

        return state with { Items = ingredients, IsLoading = false };
    }

    [ReducerMethod]
    public static ProductState ReduceDeleteItemFailureAction(ProductState state, DeleteItemFailureAction<IProductModel> action) => state with { IsLoading = false };
    #endregion

    #region UpdateProduct
    [ReducerMethod]
    public static ProductState ReduceUpdateItemAction(ProductState state, UpdateItemAction<IProductModel> action) =>
        state with { IsLoading = true };

    [ReducerMethod]
    public static ProductState ReduceUpdateItemSuccessAction(ProductState state, UpdateItemSuccessAction<IProductModel> action)
    {
        var updatedProducts = state.Items.Select(i => i.Id == action.Item.Id ? action.Item : i).ToList();
        return state with { Items = updatedProducts, IsLoading = false };
    }

    [ReducerMethod]
    public static ProductState ReduceUpdateItemFailureAction(ProductState state, UpdateItemFailureAction<IProductModel> action) =>
        state with { IsLoading = false };
    #endregion

    #region SetProductModalVisibility
    [ReducerMethod]
    public static ProductState ReduceSetProductModalVisibilityAction(ProductState state, SetProductModalVisibilityAction action) =>
        state with { ProductModalVisibility = action.IsVisible };
    #endregion
}
