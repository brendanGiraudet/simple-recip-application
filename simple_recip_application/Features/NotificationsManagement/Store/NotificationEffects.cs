using Fluxor;
using simple_recip_application.Features.Importation.Store.Actions;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Enums;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.ProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Features.RecipePlanningFeature.Store.Actions;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.UserPantryManagement.Store.Actions;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.NotificationsManagement.Store;

public class NotificationEffects
(
    ILogger<NotificationEffects> _logger,
    INotificationMessageFactory _notificationMessageFactory
)
{
    [EffectMethod]
    public async Task HandleAddNotification(AddItemAction<INotificationMessage> action, IDispatcher dispatcher)
    {
        _logger.LogInformation($"Notification ajoutée : {action.Item.Message}");

        await Task.Delay(action.Item.Duration);

        dispatcher.Dispatch(new DeleteItemAction<INotificationMessage>(action.Item));
    }

    #region Importation
    [EffectMethod]
    public async Task HandleImportSuccessAction(ImportSuccessAction action, IDispatcher dispatcher)
    {

        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.ImportSuccess, NotificationType.Success);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleImportFailureAction(ImportFailureAction action, IDispatcher dispatcher)
    {

        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.ImportFailure, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }
    #endregion

    #region Ingredients
    [EffectMethod]
    public async Task HandleLoadItemsFailureAction(LoadItemsFailureAction<IIngredientModel> action, IDispatcher dispatcher)
    {

        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.LoadIngredientErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleAddItemFailureAction(AddItemFailureAction<IIngredientModel> action, IDispatcher dispatcher)
    {

        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.AddIngredientErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleAddItemSuccessAction(AddItemSuccessAction<IIngredientModel> action, IDispatcher dispatcher)
    {

        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.AddIngredientSuccessMessage, NotificationType.Success);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleDeleteItemFailureAction(DeleteItemFailureAction<IIngredientModel> action, IDispatcher dispatcher)
    {

        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.DeleteIngredientErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleDeleteItemSuccessAction(DeleteItemSuccessAction<IIngredientModel> action, IDispatcher dispatcher)
    {

        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.DeleteIngredientSuccessMessage, NotificationType.Success);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleUpdateItemFailureAction(UpdateItemFailureAction<IIngredientModel> action, IDispatcher dispatcher)
    {

        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.UpdateIngredientErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleUpdateItemSuccessAction(UpdateItemSuccessAction<IIngredientModel> action, IDispatcher dispatcher)
    {

        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.UpdateIngredientSuccessMessage, NotificationType.Success);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }
    #endregion

    #region PlanifiedRecipe
    [EffectMethod]
    public async Task HandleLoadItemsFailureAction(LoadItemsFailureAction<IPlanifiedRecipeModel> action, IDispatcher dispatcher)
    {
        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.LoadPlanifiedRecipesErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleAddItemFailureAction(AddItemFailureAction<IPlanifiedRecipeModel> action, IDispatcher dispatcher)
    {
        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.AddPlanifiedRecipeErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleAddItemSuccessAction(AddItemSuccessAction<IPlanifiedRecipeModel> action, IDispatcher dispatcher)
    {
        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.AddPlanifiedRecipeSuccessMessage, NotificationType.Success);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleDeleteItemFailureAction(DeleteItemFailureAction<IPlanifiedRecipeModel> action, IDispatcher dispatcher)
    {
        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.DeletePlanifiedRecipeErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandlePlanifiedRecipesForTheWeekFailureAction(PlanifiedRecipesForTheWeekFailureAction action, IDispatcher dispatcher)
    {
        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.AddPlanifiedRecipeErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandlePlanifiedRecipeAutomaticalyFailureAction(PlanifiedRecipeAutomaticalyFailureAction action, IDispatcher dispatcher)
    {
        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.AddPlanifiedRecipeErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }
    #endregion

    #region Recipes
    [EffectMethod]
    public async Task HandleLoadItemsFailureAction(LoadItemsFailureAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.LoadRecipeErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleLoadItemFailureAction(LoadItemFailureAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.LoadRecipeErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleAddItemFailureAction(AddItemFailureAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.AddRecipeErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleAddItemSuccessAction(AddItemSuccessAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.AddRecipeSuccessMessage, NotificationType.Success);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleDeleteItemAction(DeleteItemAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.DeleteRecipeErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleDeleteItemSuccessAction(DeleteItemSuccessAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.DeleteRecipeSuccessMessage, NotificationType.Success);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleUpdateItemFailureAction(UpdateItemFailureAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.UpdateRecipeErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleUpdateItemSuccessAction(UpdateItemSuccessAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.UpdateRecipeSuccessMessage, NotificationType.Success);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }
    #endregion

    #region Products
    [EffectMethod]
    public async Task HandleLoadItemsFailureAction(LoadItemsFailureAction<IProductModel> action, IDispatcher dispatcher)
    {

        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.LoadProductErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleAddItemFailureAction(AddItemFailureAction<IProductModel> action, IDispatcher dispatcher)
    {

        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.AddProductErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleAddItemSuccessAction(AddItemSuccessAction<IProductModel> action, IDispatcher dispatcher)
    {

        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.AddProductSuccessMessage, NotificationType.Success);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleDeleteItemFailureAction(DeleteItemFailureAction<IProductModel> action, IDispatcher dispatcher)
    {

        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.DeleteProductErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleDeleteItemSuccessAction(DeleteItemSuccessAction<IProductModel> action, IDispatcher dispatcher)
    {

        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.DeleteProductSuccessMessage, NotificationType.Success);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleUpdateItemFailureAction(UpdateItemFailureAction<IProductModel> action, IDispatcher dispatcher)
    {

        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.UpdateProductErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleUpdateItemSuccessAction(UpdateItemSuccessAction<IProductModel> action, IDispatcher dispatcher)
    {

        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.UpdateProductSuccessMessage, NotificationType.Success);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }
    #endregion

    #region UserPantry
    [EffectMethod]
    public async Task HandleSearchProductsFailureAction(SearchProductsFailureAction action, IDispatcher dispatcher)
    {
        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.LoadProductErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }
    #endregion
}
