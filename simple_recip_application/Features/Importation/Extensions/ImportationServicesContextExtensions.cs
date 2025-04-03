using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Factories;

namespace simple_recip_application.Extensions;

public static class ImportationServicesContextExtensions
{

    public static IServiceCollection AddImportationFeatureDependencies(this IServiceCollection services)
    {
        services.AddTransient<IImportModelFactory, ImportModelFactory>();

        return services;
    }
}