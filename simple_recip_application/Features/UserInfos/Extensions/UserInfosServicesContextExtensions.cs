using simple_recip_application.Features.UserInfos.ApplicationCore.Factories;
using simple_recip_application.Features.UserInfos.Persistence.Factories;

namespace simple_recip_application.Extensions;

public static class UserInfosServicesContextExtensions
{
    public static IServiceCollection AddUserFeatureDependencies(this IServiceCollection services)
    {
        services.AddTransient<IUserInfosModelFactory, UserInfosModelFactory>();

        return services;
    }
}