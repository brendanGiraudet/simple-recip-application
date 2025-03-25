using Fluxor;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.IngredientsManagement.Store.Actions;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Enums;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Factories;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.IngredientsManagement.Store;

public class IngredientEffects
(
    IIngredientRepository _repository,
    ILogger<IngredientEffects> _logger,
    INotificationMessageFactory _notificationMessageFactory
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
            {
                dispatcher.Dispatch(new LoadItemsFailureAction<IIngredientModel>());

                var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.LoadIngredientErrorMessage, NotificationType.Error);

                dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors du chargement des ingrédients");

            dispatcher.Dispatch(new LoadItemsFailureAction<IIngredientModel>());

            var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.LoadIngredientErrorMessage, NotificationType.Error);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
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

            var message = addResult.Success ? MessagesTranslator.AddIngredientSuccessMessage : MessagesTranslator.AddIngredientErrorMessage;
            var notificationType = addResult.Success ? NotificationType.Success : NotificationType.Error;

            var notification = _notificationMessageFactory.CreateNotificationMessage(message, notificationType);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de l'ajout d'un ingrédient");

            var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.AddIngredientErrorMessage, NotificationType.Error);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

            dispatcher.Dispatch(new AddItemFailureAction<IIngredientModel>(action.Item));
        }
    }

    [EffectMethod]
    public async Task HandleDeleteIngredient(DeleteItemAction<IIngredientModel> action, IDispatcher dispatcher)
    {
        try
        {
            if (!action.Item.Id.HasValue) return;

            var ingredientResult = await _repository.GetByIdAsync(action.Item.Id.Value);
            if (!ingredientResult.Success || ingredientResult.Item == null)
            {
                var errorNotification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.DeleteIngredientErrorMessage, NotificationType.Error);

                dispatcher.Dispatch(new AddItemAction<INotificationMessage>(errorNotification));

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

            var message = deleteResult.Success ? MessagesTranslator.DeleteIngredientSuccessMessage : MessagesTranslator.DeleteIngredientErrorMessage;
            var notificationType = deleteResult.Success ? NotificationType.Success : NotificationType.Error;

            var notification = _notificationMessageFactory.CreateNotificationMessage(message, notificationType);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la suppression de l'ingrédient");

            var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.DeleteIngredientErrorMessage, NotificationType.Error);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

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

            var message = updateResult.Success ? MessagesTranslator.UpdateIngredientSuccessMessage : MessagesTranslator.UpdateIngredientErrorMessage;
            var notificationType = updateResult.Success ? NotificationType.Success : NotificationType.Error;

            var notification = _notificationMessageFactory.CreateNotificationMessage(message, notificationType);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la mise à jour");

            var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.UpdateIngredientErrorMessage, NotificationType.Error);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

            dispatcher.Dispatch(new UpdateItemFailureAction<IIngredientModel>(action.Item));
        }
    }
}
