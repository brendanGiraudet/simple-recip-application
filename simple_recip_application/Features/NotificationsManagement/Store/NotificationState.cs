using Fluxor;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.NotificationsManagement.Store;

[FeatureState]
public record class NotificationState
{
    public List<INotificationMessage> Notifications { get; init; } = new();
}
