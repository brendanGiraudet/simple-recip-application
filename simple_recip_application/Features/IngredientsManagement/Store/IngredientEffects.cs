using Fluxor;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.IngredientsManagement.Store.Actions;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.IngredientsManagement.Store;

public class IngredientEffects
(
    IIngredientRepository _repository,
    ILogger<IngredientEffects> _logger
)
{
    [EffectMethod]
    public async Task HandleLoadIngredients(LoadItemsAction<IIngredientModel> action, IDispatcher dispatcher)
    {
        try
        {
            var ingredientsResult = await _repository.GetAsync(action.Take, action.Skip, action.Predicate);

            if (ingredientsResult.Success)
                dispatcher.Dispatch(new LoadItemsSuccessAction<IIngredientModel>(ingredientsResult.Item));

            else
                dispatcher.Dispatch(new LoadItemsFailureAction<IIngredientModel>());

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors du chargement des ingrédients");

            dispatcher.Dispatch(new LoadItemsFailureAction<IIngredientModel>());
        }
    }

    [EffectMethod]
    public async Task HandleAddIngredient(AddItemAction<IIngredientModel> action, IDispatcher dispatcher)
    {
        try
        {
            var addResult = await _repository.AddAsync(action.Item);

            if (!addResult.Success)
                dispatcher.Dispatch(new AddItemFailureAction<IIngredientModel>(action.Item));

            else
            {
                dispatcher.Dispatch(new AddItemSuccessAction<IIngredientModel>(action.Item));
                dispatcher.Dispatch(new SetIngredientModalVisibilityAction(false));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de l'ajout d'un ingrédient");

            dispatcher.Dispatch(new AddItemFailureAction<IIngredientModel>(action.Item));
        }
    }

    [EffectMethod]
    public async Task HandleDeleteIngredient(DeleteItemAction<IIngredientModel> action, IDispatcher dispatcher)
    {
        try
        {
            if (!action.Item.Id.HasValue)
            {
                dispatcher.Dispatch(new DeleteItemFailureAction<IIngredientModel>(action.Item));

                return;
            }

            var ingredientResult = await _repository.GetByIdAsync(action.Item.Id.Value);
            if (!ingredientResult.Success || ingredientResult.Item == null)
            {
                dispatcher.Dispatch(new DeleteItemFailureAction<IIngredientModel>(action.Item));

                return;
            }

            var deleteResult = await _repository.DeleteAsync(ingredientResult.Item);

            if (!deleteResult.Success)
                dispatcher.Dispatch(new DeleteItemFailureAction<IIngredientModel>(action.Item));

            else
            {
                dispatcher.Dispatch(new DeleteItemSuccessAction<IIngredientModel>(action.Item));
                dispatcher.Dispatch(new SetIngredientModalVisibilityAction(false));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la suppression de l'ingrédient");

            dispatcher.Dispatch(new DeleteItemFailureAction<IIngredientModel>(action.Item));
        }
    }

    [EffectMethod]
    public async Task HandleUpdateIngredient(UpdateItemAction<IIngredientModel> action, IDispatcher dispatcher)
    {
        try
        {
            var updateResult = await _repository.UpdateAsync(action.Item);

            if (!updateResult.Success)
                dispatcher.Dispatch(new UpdateItemFailureAction<IIngredientModel>(action.Item));

            else
            {
                dispatcher.Dispatch(new UpdateItemSuccessAction<IIngredientModel>(action.Item));
                dispatcher.Dispatch(new SetIngredientModalVisibilityAction(false));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la mise à jour");

            dispatcher.Dispatch(new UpdateItemFailureAction<IIngredientModel>(action.Item));
        }
    }
}
