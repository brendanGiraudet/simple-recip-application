using simple_recip_application.Data.ApplicationCore;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Enums;

namespace simple_recip_application.Features.NotificationsManagement.ApplicationCore;

public interface INotificationMessage : IEntityBase
{
    public string Message { get; set; }
    public NotificationType Type { get; set; }
    public int Duration { get; set; }
}