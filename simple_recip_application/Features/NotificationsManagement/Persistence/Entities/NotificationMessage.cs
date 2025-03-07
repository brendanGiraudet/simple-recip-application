using simple_recip_application.Data.Persistence.Entities;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Enums;

namespace simple_recip_application.Features.NotificationsManagement.Persistence.Entites;

public class NotificationMessage : EntityBase, INotificationMessage
{
    public string Message { get; set; } = string.Empty;
    public NotificationType Type { get; set; } = NotificationType.Info;
    public int Duration { get; set; } = 5000;
}
