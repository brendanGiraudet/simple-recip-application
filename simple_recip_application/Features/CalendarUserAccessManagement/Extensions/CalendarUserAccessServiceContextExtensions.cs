using simple_recip_application.Features.CalendarManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.CalendarManagement.Persistence.Repositories;
using simple_recip_application.Features.CalendarUserAccessManagement.ApplicationCore.Factories;
using simple_recip_application.Features.CalendarUserAccessManagement.Persistence.Factories;

namespace simple_recip_application.Extensions;

public static class CalendarUserAccessServiceContextExtensions
{
    public static IServiceCollection AddCalendarUserAccessManagementDependencies(this IServiceCollection services)
    {
        services.AddTransient<ICalendarUserAccessRepository, CalendarUserAccessRepository>();
        services.AddTransient<ICalendarUserAccessModelFactory, CalendarUserAccessModelFactory>();

        return services;
    }
}