using Fluxor;
using Microsoft.Extensions.Localization;
using simple_recip_application.Features.RecipesManagement.Persistence.Repositories;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Enums;
using simple_recip_application.Features.RecipesManagement.Store.Actions;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.RecipesManagement.Store;

public class RecipeEffects
(
    IRecipeRepository _repository,
    ILogger<RecipeEffects> _logger,
    IStringLocalizer<Messages> _messagesStringLocalizer,
    INotificationMessageFactory _notificationMessageFactory
)
{
    [EffectMethod]
    public async Task HandleLoadRecipes(LoadItemsAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        try
        {
            var recipes = await _repository.GetAsync(action.Take, action.Skip, action.Predicate, action.Include);

            dispatcher.Dispatch(new LoadItemsSuccessAction<IRecipeModel>(recipes));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors du chargement des ingrédients");

            dispatcher.Dispatch(new LoadItemsFailureAction<IRecipeModel>());

            var notification = _notificationMessageFactory.CreateNotificationMessage(_messagesStringLocalizer["LoadRecipeErrorMessage"], NotificationType.Error);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
        }
    }

    [EffectMethod]
    public async Task HandleAddRecipe(AddItemAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        try
        {
            await _repository.AddAsync(action.Item);

            dispatcher.Dispatch(new AddItemSuccessAction<IRecipeModel>(action.Item));

            var notification = _notificationMessageFactory.CreateNotificationMessage(_messagesStringLocalizer["AddRecipeSuccessMessage"], NotificationType.Success);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

            dispatcher.Dispatch(new SetRecipeFormModalVisibilityAction(false));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de l'ajout d'un ingrédient");

            var notification = _notificationMessageFactory.CreateNotificationMessage(_messagesStringLocalizer["AddRecipeErrorMessage"], NotificationType.Error);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

            dispatcher.Dispatch(new AddItemFailureAction<IRecipeModel>(action.Item));
        }
    }

    [EffectMethod]
    public async Task HandleDeleteRecipe(DeleteItemAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        try
        {
            if (!action.Item.Id.HasValue) return;

            var Recipe = await _repository.GetByIdAsync(action.Item.Id.Value);
            if (Recipe != null)
            {
                await _repository.DeleteAsync(Recipe);
                dispatcher.Dispatch(new DeleteItemSuccessAction<IRecipeModel>(action.Item));

                var notification = _notificationMessageFactory.CreateNotificationMessage(_messagesStringLocalizer["DeleteRecipeSuccessMessage"], NotificationType.Success);

                dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

                dispatcher.Dispatch(new SetRecipeFormModalVisibilityAction(false));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la suppression de l'ingrédient");

            var notification = _notificationMessageFactory.CreateNotificationMessage(_messagesStringLocalizer["DeleteRecipeErrorMessage"], NotificationType.Error);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

            dispatcher.Dispatch(new DeleteItemFailureAction<IRecipeModel>(action.Item));
        }
    }

    [EffectMethod]
    public async Task HandleUpdateRecipe(UpdateItemAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        try
        {
            await _repository.UpdateAsync(action.Item);

            dispatcher.Dispatch(new UpdateItemSuccessAction<IRecipeModel>(action.Item));

            var notification = _notificationMessageFactory.CreateNotificationMessage(_messagesStringLocalizer["UpdateRecipeSuccessMessage"], NotificationType.Success);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

            dispatcher.Dispatch(new SetRecipeFormModalVisibilityAction(false));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la mise à jour");

            var notification = _notificationMessageFactory.CreateNotificationMessage(_messagesStringLocalizer["UpdateRecipeErrorMessage"], NotificationType.Error);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

            dispatcher.Dispatch(new UpdateItemFailureAction<IRecipeModel>(action.Item));
        }
    }
}
