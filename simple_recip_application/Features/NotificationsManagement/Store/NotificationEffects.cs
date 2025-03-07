using Fluxor;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Entities;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.NotificationsManagement.Store;

public class NotificationEffects
(
    ILogger<NotificationEffects> _logger
)
{
    [EffectMethod]
    public async Task HandleAddNotification(AddItemAction<INotificationMessage> action, IDispatcher dispatcher)
    {
        _logger.LogInformation($"Notification ajoutée : {action.Item.Message}");

        await Task.Delay(action.Item.Duration);

        dispatcher.Dispatch(new DeleteItemAction<INotificationMessage>(action.Item));
    }
}
