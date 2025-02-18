using Fluxor;
using Microsoft.Extensions.Localization;
using simple_recip_application.Data.Repository;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;
using simple_recip_application.Features.IngredientsManagement.Store.Actions;
using simple_recip_application.Resources;

namespace simple_recip_application.Features.IngredientsManagement.Store;

public class IngredientEffects
(
    IRepository<IngredientModel> _repository,
    ILogger<IngredientEffects> _logger,
    IStringLocalizer<Messages> _messagesStringLocalizer
)
{
    [EffectMethod]
    public async Task HandleLoadIngredients(LoadIngredientsAction action, IDispatcher dispatcher)
    {
        try
        {
            var ingredients = await _repository.GetAsync();
            dispatcher.Dispatch(new LoadIngredientsSuccessAction(ingredients.ToList()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors du chargement des ingrédients");

            dispatcher.Dispatch(new LoadIngredientsFailureAction(_messagesStringLocalizer["LoadIngredientErrorMessage"]));
        }
    }

    [EffectMethod]
    public async Task HandleAddIngredient(AddIngredientAction action, IDispatcher dispatcher)
    {
        try
        {
            await _repository.AddAsync(action.Ingredient);
            dispatcher.Dispatch(new AddIngredientSuccessAction(action.Ingredient));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de l'ajout d'un ingrédient");

            dispatcher.Dispatch(new AddIngredientFailureAction(_messagesStringLocalizer["AddIngredientErrorMessage"]));
        }
    }

    [EffectMethod]
    public async Task HandleDeleteIngredient(DeleteIngredientAction action, IDispatcher dispatcher)
    {
        try
        {
            var ingredient = await _repository.GetByIdAsync(action.IngredientId);
            if (ingredient != null)
            {
                await _repository.DeleteAsync(ingredient);
                dispatcher.Dispatch(new DeleteIngredientSuccessAction(action.IngredientId));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la suppression");

            dispatcher.Dispatch(new DeleteIngredientFailureAction(_messagesStringLocalizer["DeleteIngredientErrorMessage"]));
        }
    }

    [EffectMethod]
    public async Task HandleUpdateIngredient(UpdateIngredientAction action, IDispatcher dispatcher)
    {
        try
        {
            await _repository.UpdateAsync(action.Ingredient);
            dispatcher.Dispatch(new UpdateIngredientSuccessAction(action.Ingredient));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la mise à jour");
            
            dispatcher.Dispatch(new UpdateIngredientFailureAction(_messagesStringLocalizer["UpdateIngredientErrorMessage"]));
        }
    }
}
