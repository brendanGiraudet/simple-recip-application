using simple_recip_application.Features.UserPantryManagement.ApplicationCore.Factories;
using simple_recip_application.Features.UserPantryManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.UserPantryManagement.Persistence.Factories;
using simple_recip_application.Features.UserPantryManagement.Persistence.Repositories;

namespace simple_recip_application.Extensions;

public static class UserPantryServicesContextExtensions
{
    public static IServiceCollection AddUserPantryFeatureDependencies(this IServiceCollection services)
    {
        services.AddTransient<IUserPantryItemFactory, UserPantryItemFactory>();
        services.AddTransient<IUserPantryItemRepository, UserPantryItemRepository>();

        return services;
    }
}