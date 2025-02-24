using Fluxor;
using simple_recip_application.Features.IngredientsManagement.Store.Actions;

namespace simple_recip_application.Features.IngredientsManagement.Store;

public static class IngredientReducer
{
    #region LoadIngredients
    [ReducerMethod]
    public static IngredientState ReduceLoadIngredientsAction(IngredientState state, LoadIngredientsAction action) => state with { IsLoading = true };

    [ReducerMethod]
    public static IngredientState ReduceLoadIngredientsSuccessAction(IngredientState state, LoadIngredientsSuccessAction action) =>
        state with { Ingredients = action.Ingredients, IsLoading = false };

    [ReducerMethod]
    public static IngredientState ReduceLoadIngredientsFailureAction(IngredientState state, LoadIngredientsFailureAction action) =>
        state with { IsLoading = false };
    #endregion

    #region AddIngredient
    [ReducerMethod]
    public static IngredientState ReduceAddIngredientAction(IngredientState state, AddIngredientAction action) =>
        state with { Ingredients = state.Ingredients, IsLoading = true };

    [ReducerMethod]
    public static IngredientState ReduceAddIngredientSuccessAction(IngredientState state, AddIngredientSuccessAction action) =>
        state with { Ingredients = [.. state.Ingredients, action.Ingredient], IsLoading = false };

    [ReducerMethod]
    public static IngredientState ReduceAddIngredientFailureAction(IngredientState state, AddIngredientFailureAction action) =>
        state with { Ingredients = state.Ingredients, IsLoading = false };
    #endregion

    #region DeleteIngredient
    [ReducerMethod]
    public static IngredientState ReduceDeleteIngredientAction(IngredientState state, DeleteIngredientAction action) => state with { IsLoading = true };

    [ReducerMethod]
    public static IngredientState ReduceDeleteIngredientSuccessAction(IngredientState state, DeleteIngredientSuccessAction action)
    {
        var updatedIngredients = state.Ingredients.Where(i => i.Id != action.IngredientId).ToList();
        return state with { Ingredients = updatedIngredients, IsLoading = false };
    }

    [ReducerMethod]
    public static IngredientState ReduceDeleteIngredientFailureAction(IngredientState state, DeleteIngredientFailureAction action) => state with { IsLoading = false };
    #endregion

    #region UpdateIngredient
    [ReducerMethod]
    public static IngredientState ReduceUpdateIngredientAction(IngredientState state, UpdateIngredientAction action) =>
        state with { IsLoading = true };

    [ReducerMethod]
    public static IngredientState ReduceUpdateIngredientSuccessAction(IngredientState state, UpdateIngredientSuccessAction action)
    {
        var updatedIngredients = state.Ingredients.Select(i => i.Id == action.Ingredient.Id ? action.Ingredient : i).ToList();
        return state with { Ingredients = updatedIngredients, IsLoading = false };
    }

    [ReducerMethod]
    public static IngredientState ReduceUpdateIngredientFailureAction(IngredientState state, UpdateIngredientFailureAction action) =>
        state with { IsLoading = false };
    #endregion
}
