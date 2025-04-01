using Fluxor;
using simple_recip_application.Features.PantryIngredientManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.PantryIngredientManagement.Store.Actions;

namespace simple_recip_application.Features.PantryIngredientManagement.Store;

public class UserPantryIngredientEffects
(
    IUserPantryIngredientRepository _repository
)
{
    // Chargement
    [EffectMethod]
    public async Task HandleLoadUserPantryIngredients(LoadUserPantryIngredientsAction action, IDispatcher dispatcher)
    {
        var ingredientsResult = await _repository.GetUserPantryIngredientsAsync(action.UserId);

        if (ingredientsResult.Success)
            dispatcher.Dispatch(new LoadUserPantryIngredientsSuccessAction(ingredientsResult.Item));

        else
            dispatcher.Dispatch(new LoadUserPantryIngredientsFailureAction());
    }

    // Ajout ou mise à jour
    [EffectMethod]
    public async Task HandleAddOrUpdateUserPantryIngredient(AddOrUpdateUserPantryIngredientAction action, IDispatcher dispatcher)
    {
        var existingResult = await _repository.GetUserPantryIngredientAsync(action.Ingredient.UserId, action.Ingredient.IngredientId);
        var result = existingResult.Item is null
            ? await _repository.AddAsync(action.Ingredient)
            : await _repository.UpdateAsync(action.Ingredient);

        if (result.Success)
            dispatcher.Dispatch(new AddOrUpdateUserPantryIngredientSuccessAction(action.Ingredient));

        else
            dispatcher.Dispatch(new AddOrUpdateUserPantryIngredientFailureAction());
    }

    // Suppression
    [EffectMethod]
    public async Task HandleDeleteUserPantryIngredient(DeleteUserPantryIngredientAction action, IDispatcher dispatcher)
    {
        var result = await _repository.DeleteAsync(action.Ingredient);

        if (result.Success)
            dispatcher.Dispatch(new DeleteUserPantryIngredientSuccessAction(action.Ingredient));

        else
            dispatcher.Dispatch(new DeleteUserPantryIngredientFailureAction(action.Ingredient));
    }
}
