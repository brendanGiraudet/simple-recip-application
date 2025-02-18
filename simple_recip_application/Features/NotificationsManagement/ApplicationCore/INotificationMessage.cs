namespace simple_recip_application.Features.NotificationsManagement.ApplicationCore;

public interface INotificationMessage
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public string Type { get; set; }
    public int Duration { get; set; }
}