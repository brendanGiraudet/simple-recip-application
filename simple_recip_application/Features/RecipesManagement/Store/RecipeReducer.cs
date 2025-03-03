using Fluxor;
using simple_recip_application.Features.RecipesManagement.ApplicationCore;
using simple_recip_application.Features.RecipesManagement.Store.Actions;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipesManagement.Store;

public static class RecipeReducer
{
    #region LoadItems
    [ReducerMethod]
    public static RecipeState ReduceLoadItemsAction(RecipeState state, LoadItemsAction<IRecipeModel> action) => state with { IsLoading = true };

    [ReducerMethod]
    public static RecipeState ReduceLoadItemsSuccessAction(RecipeState state, LoadItemsSuccessAction<IRecipeModel> action)
        => state with { IsLoading = false, Items = action.Items };

    [ReducerMethod]
    public static RecipeState ReduceLoadItemsFailureAction(RecipeState state, LoadItemsFailureAction<IRecipeModel> action)
        => state with { IsLoading = false };
    #endregion

    #region AddIngredient
    [ReducerMethod]
    public static RecipeState ReduceAddItemAction(RecipeState state, AddItemAction<IRecipeModel> action) =>
        state with { Items = state.Items, IsLoading = true };

    [ReducerMethod]
    public static RecipeState ReduceAddItemSuccessAction(RecipeState state, AddItemSuccessAction<IRecipeModel> action) =>
        state with { Items = [.. state.Items, action.Item], IsLoading = false };

    [ReducerMethod]
    public static RecipeState ReduceAddItemFailureAction(RecipeState state, AddItemFailureAction<IRecipeModel> action) =>
        state with { Items = state.Items, IsLoading = false };
    #endregion

    #region DeleteIngredient
    [ReducerMethod]
    public static RecipeState ReduceDeleteItemAction(RecipeState state, DeleteItemAction<IRecipeModel> action) => state with { IsLoading = true };

    [ReducerMethod]
    public static RecipeState ReduceDeleteItemSuccessAction(RecipeState state, DeleteItemSuccessAction<IRecipeModel> action)
    {
        var recipes = state.Items.Where(i => i.Id != action.Item.Id).ToList();

        return state with { Items = recipes, IsLoading = false };
    }

    [ReducerMethod]
    public static RecipeState ReduceDeleteItemFailureAction(RecipeState state, DeleteItemFailureAction<IRecipeModel> action) => state with { IsLoading = false };
    #endregion

    #region UpdateIngredient
    [ReducerMethod]
    public static RecipeState ReduceUpdateItemAction(RecipeState state, UpdateItemAction<IRecipeModel> action) =>
        state with { IsLoading = true };

    [ReducerMethod]
    public static RecipeState ReduceUpdateItemSuccessAction(RecipeState state, UpdateItemSuccessAction<IRecipeModel> action)
    {
        var recipes = state.Items.Select(i => i.Id == action.Item.Id ? action.Item : i).ToList();
        return state with { Items = recipes, IsLoading = false };
    }

    [ReducerMethod]
    public static RecipeState ReduceUpdateItemFailureAction(RecipeState state, UpdateItemFailureAction<IRecipeModel> action) =>
        state with { IsLoading = false };
    #endregion

    #region SetRecipeFormModalVisibility
    [ReducerMethod]
    public static RecipeState ReduceSetRecipeFormModalVisibilityAction(RecipeState state, SetRecipeFormModalVisibilityAction action) =>
        state with { RecipeFormModalVisibility = action.IsVisible };
    #endregion
}