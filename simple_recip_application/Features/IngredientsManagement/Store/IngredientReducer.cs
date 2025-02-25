using Fluxor;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.IngredientsManagement.Store;

public static class IngredientReducer
{
    #region LoadIngredients
    [ReducerMethod]
    public static IngredientState ReduceLoadItemsAction(IngredientState state, LoadItemsAction<IIngredientModel> action) => state with { IsLoading = true };

    [ReducerMethod]
    public static IngredientState ReduceLoadItemsSuccessAction(IngredientState state, LoadItemsSuccessAction<IIngredientModel> action) =>
        state with { Ingredients = action.Items, IsLoading = false };

    [ReducerMethod]
    public static IngredientState ReduceLoadItemsFailureAction(IngredientState state, LoadItemsFailureAction<IIngredientModel> action) =>
        state with { IsLoading = false };
    #endregion

    #region AddIngredient
    [ReducerMethod]
    public static IngredientState ReduceAddItemAction(IngredientState state, AddItemAction<IIngredientModel> action) =>
        state with { Ingredients = state.Ingredients, IsLoading = true };

    [ReducerMethod]
    public static IngredientState ReduceAddItemSuccessAction(IngredientState state, AddItemSuccessAction<IIngredientModel> action) =>
        state with { Ingredients = [.. state.Ingredients, action.Item], IsLoading = false };

    [ReducerMethod]
    public static IngredientState ReduceAddItemFailureAction(IngredientState state, AddItemFailureAction<IIngredientModel> action) =>
        state with { Ingredients = state.Ingredients, IsLoading = false };
    #endregion

    #region DeleteIngredient
    [ReducerMethod]
    public static IngredientState ReduceDeleteItemAction(IngredientState state, DeleteItemAction<IIngredientModel> action) => state with { IsLoading = true };

    [ReducerMethod]
    public static IngredientState ReduceDeleteItemSuccessAction(IngredientState state, DeleteItemSuccessAction<IIngredientModel> action)
    {
        var ingredients = state.Ingredients.Where(i => i.Id != action.Item.Id).ToList();

        return state with { Ingredients = ingredients, IsLoading = false };
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
        var updatedIngredients = state.Ingredients.Select(i => i.Id == action.Item.Id ? action.Item : i).ToList();
        return state with { Ingredients = updatedIngredients, IsLoading = false };
    }

    [ReducerMethod]
    public static IngredientState ReduceUpdateItemFailureAction(IngredientState state, UpdateItemFailureAction<IIngredientModel> action) =>
        state with { IsLoading = false };
    #endregion
}
