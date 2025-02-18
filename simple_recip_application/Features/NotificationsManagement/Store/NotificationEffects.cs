using Fluxor;
using simple_recip_application.Features.NotificationsManagement.Store.Actions;

namespace simple_recip_application.Features.NotificationsManagement.Store;

public class NotificationEffects
(
    ILogger<NotificationEffects> _logger
)
{
    [EffectMethod]
    public async Task HandleAddNotification(AddNotificationAction action, IDispatcher dispatcher)
    {
        _logger.LogInformation($"Notification ajoutée : {action.NotificationMessage.Message}");

        await Task.Delay(action.NotificationMessage.Duration);

        dispatcher.Dispatch(new RemoveNotificationAction(action.NotificationMessage));
    }
}
