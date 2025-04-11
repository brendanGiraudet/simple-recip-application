using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.IngredientsManagement.Persistence.Factories;
using simple_recip_application.Features.IngredientsManagement.Persistence.Repositories;

namespace simple_recip_application.Extensions;

public static class IngredientServicesContextExtensions
{
    public static IServiceCollection AddIngredientFeatureDependencies(this IServiceCollection services)
    {
        services.AddTransient<IIngredientFactory, IngredientFactory>();
        services.AddScoped<IIngredientRepository, IngredientRepository>();

        return services;
    }
}