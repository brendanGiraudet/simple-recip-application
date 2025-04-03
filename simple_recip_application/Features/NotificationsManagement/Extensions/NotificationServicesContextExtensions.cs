using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.NotificationsManagement.Persistence.Factories;

namespace simple_recip_application.Extensions;

public static class NotificationServicesContextExtensions
{
    public static IServiceCollection AddNotificationFeatureDependencies(this IServiceCollection services)
    {
        services.AddTransient<INotificationMessageFactory, NotificationMessageFactory>();

        return services;
    }
}