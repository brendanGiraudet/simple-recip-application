using Fluxor;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.NotificationsManagement.Store;

public static class NotificationReducer
{
    [ReducerMethod]
    public static NotificationState ReduceAddItemAction(NotificationState state, AddItemAction<INotificationMessage> action)
    {
        return state with { Notifications = [.. state.Notifications, action.Item] };
    }

    [ReducerMethod]
    public static NotificationState ReduceRemoveNotification(NotificationState state, DeleteItemAction<INotificationMessage> action)
    {
        return state with { Notifications = state.Notifications.Where(n => n.Id != action.Item.Id).ToList() };
    }
}
