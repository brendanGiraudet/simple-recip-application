using Fluxor;
using simple_recip_application.Features.RecipesManagement.Persistence.Repositories;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Enums;
using simple_recip_application.Features.RecipesManagement.Store.Actions;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Entities;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Constants;

namespace simple_recip_application.Features.RecipesManagement.Store;

public class RecipeEffects
(
    IRecipeRepository _repository,
    ILogger<RecipeEffects> _logger,
    INotificationMessageFactory _notificationMessageFactory,
    NavigationManager _navigationManager
)
{
    [EffectMethod]
    public async Task HandleLoadRecipes(LoadItemsAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        try
        {
            var recipesResult = await _repository.GetAsync(action.Take, action.Skip, action.Predicate);

            if (!recipesResult.Success)
            {
                dispatcher.Dispatch(new LoadItemsFailureAction<IRecipeModel>());

                var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.LoadRecipeErrorMessage, NotificationType.Error);

                dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
            }
            else
                dispatcher.Dispatch(new LoadItemsSuccessAction<IRecipeModel>(recipesResult.Item!));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors du chargement des ingrédients");

            dispatcher.Dispatch(new LoadItemsFailureAction<IRecipeModel>());

            var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.LoadRecipeErrorMessage, NotificationType.Error);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
        }
    }

    [EffectMethod]
    public async Task HandleLoadRecipe(LoadItemAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        try
        {
            var recipeResult = await _repository.GetByIdAsync(action.Id);

            if (!recipeResult.Success)
            {
                dispatcher.Dispatch(new LoadItemFailureAction<IRecipeModel>());

                var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.LoadRecipeErrorMessage, NotificationType.Error);

                dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
            }
            else
                dispatcher.Dispatch(new LoadItemSuccessAction<IRecipeModel>(recipeResult.Item));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors du chargement de la recette");

            dispatcher.Dispatch(new LoadItemFailureAction<IRecipeModel>());

            var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.LoadRecipeErrorMessage, NotificationType.Error);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
        }
    }

    [EffectMethod]
    public async Task HandleAddRecipe(AddItemAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        try
        {
            var addResult = await _repository.AddAsync(action.Item);

            if (!addResult.Success)
                dispatcher.Dispatch(new AddItemFailureAction<IRecipeModel>(action.Item));

            else
            {
                dispatcher.Dispatch(new AddItemSuccessAction<IRecipeModel>(action.Item));
                dispatcher.Dispatch(new SetRecipeFormModalVisibilityAction(false));
            }

            var message = addResult.Success ? MessagesTranslator.AddRecipeSuccessMessage : MessagesTranslator.AddRecipeErrorMessage;
            var notificationType = addResult.Success ? NotificationType.Success : NotificationType.Error;

            var notification = _notificationMessageFactory.CreateNotificationMessage(message, notificationType);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de l'ajout d'un ingrédient");

            var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.AddRecipeErrorMessage, NotificationType.Error);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

            dispatcher.Dispatch(new AddItemFailureAction<IRecipeModel>(action.Item));
        }
    }

    [EffectMethod]
    public async Task HandleDeleteRecipe(DeleteItemSuccessAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        _navigationManager.NavigateTo(PageUrlsConstants.RecipesPage);
    }
    
    [EffectMethod]
    public async Task HandleDeleteRecipe(DeleteItemAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        try
        {
            if (!action.Item.Id.HasValue) return;

            var recipeResult = await _repository.GetByIdAsync(action.Item.Id.Value);
            if (!recipeResult.Success || recipeResult.Item == null)
            {
                var errorNotification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.DeleteRecipeErrorMessage, NotificationType.Error);

                dispatcher.Dispatch(new AddItemAction<INotificationMessage>(errorNotification));

                dispatcher.Dispatch(new DeleteItemFailureAction<IRecipeModel>(action.Item));

                return;
            }

            var deleteResult = await _repository.DeleteAsync(recipeResult.Item);

            if (deleteResult.Success)
                dispatcher.Dispatch(new DeleteItemFailureAction<IRecipeModel>(action.Item));

            else
            {
                dispatcher.Dispatch(new DeleteItemSuccessAction<IRecipeModel>(action.Item));
                dispatcher.Dispatch(new SetRecipeFormModalVisibilityAction(false));
            }

            var message = deleteResult.Success ? MessagesTranslator.DeleteRecipeSuccessMessage : MessagesTranslator.DeleteRecipeErrorMessage;
            var notificationType = deleteResult.Success ? NotificationType.Success : NotificationType.Error;

            var notification = _notificationMessageFactory.CreateNotificationMessage(message, notificationType);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la suppression de l'ingrédient");

            var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.DeleteRecipeErrorMessage, NotificationType.Error);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

            dispatcher.Dispatch(new DeleteItemFailureAction<IRecipeModel>(action.Item));
        }
    }

    [EffectMethod]
    public async Task HandleUpdateRecipe(UpdateItemAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        try
        {
            var updateResult = await _repository.UpdateAsync(action.Item);

            if (!updateResult.Success)
                dispatcher.Dispatch(new UpdateItemFailureAction<IRecipeModel>(action.Item));

            else
            {
                dispatcher.Dispatch(new UpdateItemSuccessAction<IRecipeModel>(action.Item));
                dispatcher.Dispatch(new SetRecipeFormModalVisibilityAction(false));
            }

            var message = updateResult.Success ? MessagesTranslator.UpdateRecipeSuccessMessage : MessagesTranslator.UpdateRecipeErrorMessage;
            var notificationType = updateResult.Success ? NotificationType.Success : NotificationType.Error;

            var notification = _notificationMessageFactory.CreateNotificationMessage(message, notificationType);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la mise à jour");

            var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.UpdateRecipeErrorMessage, NotificationType.Error);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

            dispatcher.Dispatch(new UpdateItemFailureAction<IRecipeModel>(action.Item));
        }
    }
}
