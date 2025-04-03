using simple_recip_application.Features.ProductsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.ProductsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.ProductsManagement.Persistence.Factories;
using simple_recip_application.Features.ProductsManagement.Persistence.Repositories;

namespace simple_recip_application.Extensions;

public static class ProductServicesContextExtensions
{
    public static IServiceCollection AddProductFeatureDependencies(this IServiceCollection services)
    {
        services.AddTransient<IProductFactory, ProductFactory>();
        services.AddTransient<IProductRepository, ProductRepository>();

        return services;
    }
}