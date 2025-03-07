using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Enums;

namespace simple_recip_application.Features.NotificationsManagement.ApplicationCore.Factories;

public interface INotificationMessageFactory
{
    INotificationMessage CreateNotificationMessage(string message, NotificationType type, int? duration = null);
}
