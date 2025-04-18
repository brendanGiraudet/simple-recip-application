using Fluxor;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.EqualityComparers;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.IngredientsManagement.Store;

public static class IngredientReducer
{
    #region LoadIngredients
    [ReducerMethod]
    public static IngredientState ReduceLoadItemsAction(IngredientState state, LoadItemsAction<IIngredientModel> action) => state with { IsLoading = true, Take = action.Take, Skip = action.Skip };

    [ReducerMethod]
    public static IngredientState ReduceLoadItemsSuccessAction(IngredientState state, LoadItemsSuccessAction<IIngredientModel> action) =>
        state with { Items = action.Items, IsLoading = false };

    [ReducerMethod]
    public static IngredientState ReduceLoadItemsFailureAction(IngredientState state, LoadItemsFailureAction<IIngredientModel> action) =>
        state with { IsLoading = false };
    #endregion

    #region AddIngredient
    [ReducerMethod]
    public static IngredientState ReduceAddItemAction(IngredientState state, AddItemAction<IIngredientModel> action) =>
        state with { Items = state.Items, IsLoading = true };

    [ReducerMethod]
    public static IngredientState ReduceAddItemSuccessAction(IngredientState state, AddItemSuccessAction<IIngredientModel> action) =>
        state with { Items = [.. state.Items, action.Item], IsLoading = false };

    [ReducerMethod]
    public static IngredientState ReduceAddItemFailureAction(IngredientState state, AddItemFailureAction<IIngredientModel> action) =>
        state with { Items = state.Items, IsLoading = false };
    #endregion

    #region DeleteIngredient
    [ReducerMethod]
    public static IngredientState ReduceDeleteItemAction(IngredientState state, DeleteItemAction<IIngredientModel> action) => state with { IsLoading = true };

    [ReducerMethod]
    public static IngredientState ReduceDeleteItemSuccessAction(IngredientState state, DeleteItemSuccessAction<IIngredientModel> action)
    {
        var ingredients = state.Items.Where(i => !new IngredientEqualityComparer().Equals(i, action.Item)).ToList();

        return state with { Items = ingredients, IsLoading = false };
    }

    [ReducerMethod]
    public static IngredientState ReduceDeleteItemFailureAction(IngredientState state, DeleteItemFailureAction<IIngredientModel> action) => state with { IsLoading = false };
    #endregion

    #region UpdateIngredient
    [ReducerMethod]
    public static IngredientState ReduceUpdateItemAction(IngredientState state, UpdateItemAction<IIngredientModel> action) =>
        state with { IsLoading = true };

    [ReducerMethod]
    public static IngredientState ReduceUpdateItemSuccessAction(IngredientState state, UpdateItemSuccessAction<IIngredientModel> action)
    {
        var updatedIngredients = state.Items.Select(i => i.Id == action.Item.Id ? action.Item : i).ToList();
        return state with { Items = updatedIngredients, IsLoading = false };
    }

    [ReducerMethod]
    public static IngredientState ReduceUpdateItemFailureAction(IngredientState state, UpdateItemFailureAction<IIngredientModel> action) =>
        state with { IsLoading = false };
    #endregion

    #region SetIngredientModalVisibility
    [ReducerMethod]
    public static IngredientState ReduceSetIngredientModalVisibilityAction(IngredientState state, SetFormModalVisibilityAction<IIngredientModel> action) =>
        state with { FormModalVisibility = action.IsVisible };
    #endregion
}
