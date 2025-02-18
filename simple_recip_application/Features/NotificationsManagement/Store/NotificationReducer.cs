using Fluxor;
using simple_recip_application.Features.NotificationsManagement.Store.Actions;

namespace simple_recip_application.Features.NotificationsManagement.Store;

public static class NotificationReducer
{
    [ReducerMethod]
    public static NotificationState ReduceAddNotification(NotificationState state, AddNotificationAction action)
    {
        return state with { Notifications = [.. state.Notifications, action.NotificationMessage] };
    }

    [ReducerMethod]
    public static NotificationState ReduceRemoveNotification(NotificationState state, RemoveNotificationAction action)
    {
        return state with { Notifications = state.Notifications.Where(n => n.Id != action.NotificationMessage.Id).ToList() };
    }
}
