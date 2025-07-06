using simple_recip_application.Features.RecipesManagement.ApplicationCore.Factories;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.RecipesManagement.Persistence.Factories;
using simple_recip_application.Features.RecipesManagement.Persistence.Repositories;

namespace simple_recip_application.Extensions;

public static class RecipesServicesContextExtensions
{
    public static IServiceCollection AddRecipesFeatureDependencies(this IServiceCollection services)
    {
        services.AddTransient<IRecipeIngredientFactory, RecipeIngredientFactory>();
        services.AddTransient<IRecipeTagFactory, RecipeTagFactory>();
        services.AddTransient<IRecipeFactory, RecipeFactory>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();

        return services;
    }
}