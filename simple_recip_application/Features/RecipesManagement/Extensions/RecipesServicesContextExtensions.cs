using simple_recip_application.Features.RecipesManagement.ApplicationCore.Factories;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Services;
using simple_recip_application.Features.RecipesManagement.Persistence.Factories;
using simple_recip_application.Features.RecipesManagement.Persistence.Services;

namespace simple_recip_application.Extensions;

public static class RecipesServicesContextExtensions
{
    public static IServiceCollection AddDIForRecipesFeature(this IServiceCollection services)
    {
        services.AddTransient<IShoppingListGeneratorService, ShoppingListGeneratorService>();
        services.AddTransient<IRecipeIngredientFactory, RecipeIngredientFactory>();
        services.AddTransient<IRecipeFactory, RecipeFactory>();

        return services;
    }
}