using Fluxor;
using Microsoft.Extensions.Localization;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.IngredientsManagement.Persistence.Repositories;
using simple_recip_application.Features.IngredientsManagement.Store.Actions;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Enums;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.NotificationsManagement.Persistence.Entites;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.IngredientsManagement.Store;

public class IngredientEffects
(
    IIngredientRepository _repository,
    ILogger<IngredientEffects> _logger,
    IStringLocalizer<Messages> _messagesStringLocalizer,
    INotificationMessageFactory _notificationMessageFactory
)
{
    [EffectMethod]
    public async Task HandleLoadIngredients(LoadItemsAction<IIngredientModel> action, IDispatcher dispatcher)
    {
        try
        {
            var ingredients = await _repository.GetAsync(action.Take, action.Skip, action.Predicate);

            dispatcher.Dispatch(new LoadItemsSuccessAction<IIngredientModel>(ingredients));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors du chargement des ingrédients");

            dispatcher.Dispatch(new LoadItemsFailureAction<IIngredientModel>());

            var notification = _notificationMessageFactory.CreateNotificationMessage(_messagesStringLocalizer["LoadIngredientErrorMessage"], NotificationType.Error);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
        }
    }

    [EffectMethod]
    public async Task HandleAddIngredient(AddItemAction<IIngredientModel> action, IDispatcher dispatcher)
    {
        try
        {
            await _repository.AddAsync(action.Item);

            dispatcher.Dispatch(new AddItemSuccessAction<IIngredientModel>(action.Item));

            var notification = _notificationMessageFactory.CreateNotificationMessage(_messagesStringLocalizer["AddIngredientSuccessMessage"], NotificationType.Success);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

            dispatcher.Dispatch(new SetIngredientModalVisibilityAction(false));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de l'ajout d'un ingrédient");

            var notification = _notificationMessageFactory.CreateNotificationMessage(_messagesStringLocalizer["AddIngredientErrorMessage"], NotificationType.Error);

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

            var ingredient = await _repository.GetByIdAsync(action.Item.Id.Value);
            if (ingredient != null)
            {
                await _repository.DeleteAsync(ingredient);
                dispatcher.Dispatch(new DeleteItemSuccessAction<IIngredientModel>(action.Item));

                var notification = _notificationMessageFactory.CreateNotificationMessage(_messagesStringLocalizer["DeleteIngredientSuccessMessage"], NotificationType.Success);

                dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

                dispatcher.Dispatch(new SetIngredientModalVisibilityAction(false));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la suppression de l'ingrédient");

            var notification = _notificationMessageFactory.CreateNotificationMessage(_messagesStringLocalizer["DeleteIngredientErrorMessage"], NotificationType.Error);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

            dispatcher.Dispatch(new DeleteItemFailureAction<IIngredientModel>(action.Item));
        }
    }

    [EffectMethod]
    public async Task HandleUpdateIngredient(UpdateItemAction<IIngredientModel> action, IDispatcher dispatcher)
    {
        try
        {
            await _repository.UpdateAsync(action.Item);

            dispatcher.Dispatch(new UpdateItemSuccessAction<IIngredientModel>(action.Item));

            var notification = _notificationMessageFactory.CreateNotificationMessage(_messagesStringLocalizer["UpdateIngredientSuccessMessage"], NotificationType.Success);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

            dispatcher.Dispatch(new SetIngredientModalVisibilityAction(false));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la mise à jour");

            var notification = _notificationMessageFactory.CreateNotificationMessage(_messagesStringLocalizer["UpdateIngredientErrorMessage"], NotificationType.Error);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

            dispatcher.Dispatch(new UpdateItemFailureAction<IIngredientModel>(action.Item));
        }
    }
}
