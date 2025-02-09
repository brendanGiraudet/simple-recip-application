using Fluxor;
using simple_recip_application.Features.IngredientsManagement.Store.Actions;

namespace simple_recip_application.Features.IngredientsManagement.Store;

public static class IngredientReducer
{
    [ReducerMethod]
    public static IngredientState ReduceLoadIngredients(IngredientState state, LoadIngredientsAction action) =>
        new() { IsLoading = true };

    [ReducerMethod]
    public static IngredientState ReduceLoadIngredientsSuccess(IngredientState state, LoadIngredientsSuccessAction action) =>
        new() { Ingredients = action.Ingredients, IsLoading = false };

    [ReducerMethod]
    public static IngredientState ReduceLoadIngredientsFailure(IngredientState state, LoadIngredientsFailureAction action) =>
        new() { IsLoading = false, ErrorMessage = action.ErrorMessage };

    [ReducerMethod]
    public static IngredientState ReduceAddIngredient(IngredientState state, AddIngredientAction action) =>
        new() { Ingredients = state.Ingredients, IsLoading = true };

    [ReducerMethod]
    public static IngredientState ReduceAddIngredientSuccess(IngredientState state, AddIngredientSuccessAction action) =>
        new() { Ingredients = [.. state.Ingredients, action.Ingredient], IsLoading = false };

    [ReducerMethod]
    public static IngredientState ReduceAddIngredientFailure(IngredientState state, AddIngredientFailureAction action) =>
        new() { Ingredients = state.Ingredients, IsLoading = false, ErrorMessage = action.ErrorMessage };
}
