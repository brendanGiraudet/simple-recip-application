using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.IngredientsManagement.Persistence.Factories;

namespace simple_recip_application.Extensions;

public static class IngredientServicesContextExtensions
{
    public static IServiceCollection AddIngredientFeatureDependencies(this IServiceCollection services)
    {
        services.AddTransient<IIngredientFactory, IngredientFactory>();

        return services;
    }
}