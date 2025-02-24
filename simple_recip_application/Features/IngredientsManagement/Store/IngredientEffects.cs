using Fluxor;
using Microsoft.Extensions.Localization;
using simple_recip_application.Features.IngredientsManagement.Persistence.Repositories;
using simple_recip_application.Features.IngredientsManagement.Store.Actions;
using simple_recip_application.Features.NotificationsManagement.Persistence.Entites;
using simple_recip_application.Features.NotificationsManagement.Store.Actions;
using simple_recip_application.Resources;

namespace simple_recip_application.Features.IngredientsManagement.Store;

public class IngredientEffects
(
    IIngredientRepository _repository,
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

            dispatcher.Dispatch(new LoadIngredientsFailureAction());

            var notification = new NotificationMessage()
            {
                Message = _messagesStringLocalizer["LoadIngredientErrorMessage"],
                Type = "danger"
            };

            dispatcher.Dispatch(new AddNotificationAction(notification));
        }
    }

    [EffectMethod]
    public async Task HandleAddIngredient(AddIngredientAction action, IDispatcher dispatcher)
    {
        try
        {
            await _repository.AddAsync(action.Ingredient);
            
            dispatcher.Dispatch(new AddIngredientSuccessAction(action.Ingredient));

            var notification = new NotificationMessage()
            {
                Message = _messagesStringLocalizer["AddIngredientSuccessMessage"],
                Type = "success"
            };

            dispatcher.Dispatch(new AddNotificationAction(notification));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de l'ajout d'un ingrédient");

            var notification = new NotificationMessage()
            {
                Message = _messagesStringLocalizer["AddIngredientErrorMessage"],
                Type = "danger"
            };

            dispatcher.Dispatch(new AddNotificationAction(notification));

            dispatcher.Dispatch(new AddIngredientFailureAction());
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

                var notification = new NotificationMessage()
                {
                    Message = _messagesStringLocalizer["DeleteIngredientSuccessMessage"],
                    Type = "success"
                };

                dispatcher.Dispatch(new AddNotificationAction(notification));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la suppression de l'ingrédient");

            var notification = new NotificationMessage()
            {
                Message = _messagesStringLocalizer["DeleteIngredientErrorMessage"],
                Type = "danger"
            };

            dispatcher.Dispatch(new AddNotificationAction(notification));

            dispatcher.Dispatch(new DeleteIngredientFailureAction());
        }
    }

    [EffectMethod]
    public async Task HandleUpdateIngredient(UpdateIngredientAction action, IDispatcher dispatcher)
    {
        try
        {
            await _repository.UpdateAsync(action.Ingredient);

            dispatcher.Dispatch(new UpdateIngredientSuccessAction(action.Ingredient));

            var notification = new NotificationMessage()
            {
                Message = _messagesStringLocalizer["UpdateIngredientSuccessMessage"],
                Type = "success"
            };

            dispatcher.Dispatch(new AddNotificationAction(notification));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la mise à jour");

            var notification = new NotificationMessage()
            {
                Message = _messagesStringLocalizer["UpdateIngredientErrorMessage"],
                Type = "danger"
            };

            dispatcher.Dispatch(new AddNotificationAction(notification));
            
            dispatcher.Dispatch(new UpdateIngredientFailureAction());
        }
    }
}
