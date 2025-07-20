using simple_recip_application.Features.CalendarManagement.ApplicationCore.Factories;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.CalendarManagement.Persistence.Factories;
using simple_recip_application.Features.CalendarManagement.Persistence.Repositories;

namespace simple_recip_application.Extensions;

public static class CalendarServiceContextExtensions
{
    public static IServiceCollection AddCalendarManagementDependencies(this IServiceCollection services)
    {
        services.AddTransient<ICalendarModelFactory, CalendarModelFactory>();
        services.AddScoped<ICalendarRepository, CalendarRepository>();

        services.AddTransient<ICalendarUserAccessModelFactory, CalendarUserAccessModelFactory>();

        return services;
    }
}