using Fluxor;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Enums;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Repositories;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipesManagement.Store;

public class PlanifiedRecipeEffects
(
    IPlanifiedRecipeRepository _planifiedRecipeRepository,
    INotificationMessageFactory _notificationMessageFactory
)
{
    [EffectMethod]
    public async Task HandleLoadItemsAction(LoadItemsAction<IPlanifiedRecipeModel> action, IDispatcher dispatcher)
    {
        var result = await _planifiedRecipeRepository.GetAsync(action.Take, action.Skip, action.Predicate);

        if (result.Success)
            dispatcher.Dispatch(new LoadItemsSuccessAction<IPlanifiedRecipeModel>(result.Item));

        else
            dispatcher.Dispatch(new LoadItemsFailureAction<IPlanifiedRecipeModel>());
    }

    [EffectMethod]
    public async Task HandleLoadItemsFailureAction(LoadItemsFailureAction<IPlanifiedRecipeModel> action, IDispatcher dispatcher)
    {
        var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.LoadPlanifiedRecipesErrorMessage, NotificationType.Error);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

        await Task.CompletedTask;
    }

    [EffectMethod]
    public async Task HandleAddItemAction(AddItemAction<IPlanifiedRecipeModel> action, IDispatcher dispatcher)
    {
        var result = await _planifiedRecipeRepository.AddAsync(action.Item);

        var message = MessagesTranslator.AddPlanifiedRecipeErrorMessage;
        var notificationType = NotificationType.Error;

        if (result.Success)
        {
            dispatcher.Dispatch(new AddItemSuccessAction<IPlanifiedRecipeModel>(action.Item));
            message = MessagesTranslator.AddPlanifiedRecipeSuccessMessage;
            notificationType = NotificationType.Success;
        }

        else
            dispatcher.Dispatch(new AddItemFailureAction<IPlanifiedRecipeModel>(action.Item));


        var notification = _notificationMessageFactory.CreateNotificationMessage(message, notificationType);

        dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
    }

    [EffectMethod]
    public async Task HandleDeleteItemAction(DeleteItemAction<IPlanifiedRecipeModel> action, IDispatcher dispatcher)
    {
        var result = await _planifiedRecipeRepository.DeleteAsync(action.Item);

        if (result.Success)
            dispatcher.Dispatch(new DeleteItemSuccessAction<IPlanifiedRecipeModel>(action.Item));

        else
        {
            dispatcher.Dispatch(new DeleteItemFailureAction<IPlanifiedRecipeModel>(action.Item));

            var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.AddPlanifiedRecipeErrorMessage, NotificationType.Error);
            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
        }
    }
}
