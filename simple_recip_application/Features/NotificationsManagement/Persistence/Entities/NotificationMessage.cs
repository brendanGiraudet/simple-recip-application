using simple_recip_application.Data.ApplicationCore;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore;

namespace simple_recip_application.Features.NotificationsManagement.Persistence.Entites;

public class NotificationMessage : EntityBase, INotificationMessage
{
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = "info";
    public int Duration { get; set; } = 3000;
}
