using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Factories;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Services;
using simple_recip_application.Features.ShoppingListManagement.Persistence.Factories;
using simple_recip_application.Features.ShoppingListManagement.Persistence.Repositories;
using simple_recip_application.Features.ShoppingListManagement.Persistence.Services;

namespace simple_recip_application.Extensions;

public static class ShoppingListManagementServicesCollectionExtensions
{
    public static IServiceCollection AddShoppingListManagementDependencies(this IServiceCollection services)
    {
        services.AddTransient<IShoppingListItemModelFactory, ShoppingListItemModelFactory>();
        services.AddTransient<IShoppingListRepository, ShoppingListRepository>();
        services.AddTransient<IShoppingListGeneratorService, ShoppingListGeneratorService>();

        return services;
    }
}