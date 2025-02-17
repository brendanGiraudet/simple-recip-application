using Fluxor;
using simple_recip_application.Data.Repository;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;
using simple_recip_application.Features.IngredientsManagement.Store.Actions;

namespace simple_recip_application.Features.IngredientsManagement.Store;

public class IngredientEffects(IRepository<IngredientModel> _repository, ILogger<IngredientEffects> _logger)
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
            _logger.LogError($"Erreur lors du chargement des ingrédients : {ex.Message}");
            dispatcher.Dispatch(new LoadIngredientsFailureAction(ex.Message));
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
            _logger.LogError($"Erreur lors de l'ajout d'un ingrédient : {ex.Message}");
            dispatcher.Dispatch(new AddIngredientFailureAction(ex.Message));
        }
    }
}
