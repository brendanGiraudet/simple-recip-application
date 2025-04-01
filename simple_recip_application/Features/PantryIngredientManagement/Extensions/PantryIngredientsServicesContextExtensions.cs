using simple_recip_application.Features.PantryIngredientManagement.ApplicationCore.Factories;
using simple_recip_application.Features.PantryIngredientManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.PantryIngredientManagement.Persistence.Factories;
using simple_recip_application.Features.PantryIngredientManagement.Persistence.Repositories;

namespace simple_recip_application.Extensions;

public static class PantryIngredientsServicesContextExtensions
{
    public static IServiceCollection AddDIForPantryIngredientManagementFeature(this IServiceCollection services)
    {
        services.AddTransient<IUserPantryIngredientFactory, UserPantryIngredientFactory>();
        services.AddTransient<IUserPantryIngredientRepository, UserPantryIngredientRepository>();

        return services;
    }
}