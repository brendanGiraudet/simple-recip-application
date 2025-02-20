using simple_recip_application.Data.ApplicationCore;

namespace simple_recip_application.Features.NotificationsManagement.ApplicationCore;

public interface INotificationMessage : IEntityBase
{
    public string Message { get; set; }
    public string Type { get; set; }
    public int Duration { get; set; }
}