using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore;
using simple_recip_application.Features.NotificationsManagement.Store;
using simple_recip_application.Features.NotificationsManagement.Store.Actions;

namespace simple_recip_application.Features.NotificationsManagement.UserInterfaces.Components.Notifications;

public class NotificationBase : Fluxor.Blazor.Web.Components.FluxorComponent
{
    [Inject] protected IDispatcher Dispatcher { get; set; } = default!;
    [Inject] protected IState<NotificationState> NotificationState { get; set; } = default!;

    protected void RemoveNotification(INotificationMessage notification)
    {
        Dispatcher.Dispatch(new RemoveNotificationAction(notification));
    }
}