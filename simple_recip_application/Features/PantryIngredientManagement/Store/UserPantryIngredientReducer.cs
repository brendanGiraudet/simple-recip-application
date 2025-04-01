using Fluxor;
using simple_recip_application.Features.PantryIngredientManagement.ApplicationCore.EqualityComparers;
using simple_recip_application.Features.PantryIngredientManagement.Store.Actions;

namespace simple_recip_application.Features.PantryIngredientManagement.Store;

public static class UserPantryIngredientReducer
{
    #region Load

    [ReducerMethod]
    public static UserPantryIngredientState ReduceLoadUserPantryIngredientsAction(UserPantryIngredientState state, LoadUserPantryIngredientsAction action)
        => state with { IsLoading = true, UserId = action.UserId };

    [ReducerMethod]
    public static UserPantryIngredientState ReduceLoadUserPantryIngredientsSuccessAction(UserPantryIngredientState state, LoadUserPantryIngredientsSuccessAction action)
        => state with { IsLoading = false, Items = action.Ingredients };

    [ReducerMethod]
    public static UserPantryIngredientState ReduceLoadUserPantryIngredientsFailureAction(UserPantryIngredientState state, LoadUserPantryIngredientsFailureAction action)
        => state with { IsLoading = false };

    #endregion

    #region AddOrUpdate

    [ReducerMethod]
    public static UserPantryIngredientState ReduceAddOrUpdateUserPantryIngredientAction(UserPantryIngredientState state, AddOrUpdateUserPantryIngredientAction action)
        => state with { IsLoading = true };

    [ReducerMethod]
    public static UserPantryIngredientState ReduceAddOrUpdateUserPantryIngredientSuccessAction(UserPantryIngredientState state, AddOrUpdateUserPantryIngredientSuccessAction action)
    {
        var updatedItems = state.Items.Where(i => i.IngredientId != action.Ingredient.IngredientId).Append(action.Ingredient);

        return state with { IsLoading = false, Items = updatedItems };
    }

    [ReducerMethod]
    public static UserPantryIngredientState ReduceAddOrUpdateUserPantryIngredientFailureAction(UserPantryIngredientState state, AddOrUpdateUserPantryIngredientFailureAction action)
        => state with { IsLoading = false };

    #endregion

    #region Delete

    [ReducerMethod]
    public static UserPantryIngredientState ReduceDeleteUserPantryIngredientAction(UserPantryIngredientState state, DeleteUserPantryIngredientAction action)
        => state with { IsLoading = true };

    [ReducerMethod]
    public static UserPantryIngredientState ReduceDeleteUserPantryIngredientSuccessAction(UserPantryIngredientState state, DeleteUserPantryIngredientSuccessAction action)
        => state with { IsLoading = false, Items = state.Items.Where(i => !new UserPantryIngredientEqualityComparer().Equals(i, action.Ingredient)) };

    [ReducerMethod]
    public static UserPantryIngredientState ReduceDeleteUserPantryIngredientFailureAction(UserPantryIngredientState state, DeleteUserPantryIngredientFailureAction action)
        => state with { IsLoading = false };

    #endregion
}
