using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.HouseholdProductsManagement.Persistence.Factories;
using simple_recip_application.Features.HouseholdProductsManagement.Persistence.Repositories;

namespace simple_recip_application.Extensions;

public static class HouseholdProductServicesContextExtensions
{
    public static IServiceCollection AddHouseholdProductFeatureDependencies(this IServiceCollection services)
    {
        services.AddTransient<IHouseholdProductFactory, HouseholdProductFactory>();
        services.AddTransient<IHouseholdProductRepository, HouseholdProductRepository>();

        return services;
    }
}