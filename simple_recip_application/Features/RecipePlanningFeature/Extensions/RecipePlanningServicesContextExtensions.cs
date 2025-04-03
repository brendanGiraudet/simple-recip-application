using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Factories;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Services;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Factories;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Services;

namespace simple_recip_application.Extensions;

public static class RecipePlanningServicesContextExtensions
{

    public static IServiceCollection AddRecipePlanningFeatureDependencies(this IServiceCollection services)
    {
        services.AddTransient<IRecipePlanifierService, RecipePlanifierService>();
        services.AddTransient<IPlanifiedRecipeModelFactory, PlanifiedRecipeModelFactory>();

        return services;
    }
}