using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Factories;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Repositories;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Services;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Factories;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Repositories;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Services;
using simple_recip_application.Features.RecipesManagement.Persistence.Repositories;

namespace simple_recip_application.Extensions;

public static class RecipePlanningServicesContextExtensions
{

    public static IServiceCollection AddRecipePlanningFeatureDependencies(this IServiceCollection services)
    {
        services.AddTransient<IRecipePlanifierService, RecipePlanifierService>();
        
        services.AddTransient<IPlanifiedRecipeModelFactory, PlanifiedRecipeModelFactory>();
        services.AddScoped<IPlanifiedRecipeRepository, PlanifiedRecipeRepository>();
        
        services.AddTransient<ICalendarModelFactory, CalendarModelFactory>();
        services.AddScoped<ICalendarRepository, CalendarRepository>();
        
        services.AddTransient<ICalendarUserAccessModelFactory, CalendarUserAccessModelFactory>();

        return services;
    }
}