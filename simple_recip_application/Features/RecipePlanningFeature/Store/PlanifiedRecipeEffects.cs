using Fluxor;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Enums;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Repositories;
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
        {
            dispatcher.Dispatch(new LoadItemsFailureAction<IPlanifiedRecipeModel>());
            var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.LoadPlanifiedRecipesErrorMessage, NotificationType.Error);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
        }
    }
}
