using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Enums;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.NotificationsManagement.Persistence.Entites;

namespace simple_recip_application.Features.NotificationsManagement.Persistence.Factories;

public class NotificationMessageFactory : INotificationMessageFactory
{
    public INotificationMessage CreateNotificationMessage(string message, NotificationType type, int? duration = null)
    {
        return new NotificationMessage()
        {
            Message = message,
            Type = type,
            Duration = duration ?? 5000
        };
    }
}
