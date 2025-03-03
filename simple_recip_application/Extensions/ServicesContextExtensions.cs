using simple_recip_application.Settings;

namespace simple_recip_application.Extensions;

public static class ServicesContextExtensions
{
    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FileSettings>(configuration.GetSection(nameof(FileSettings)));
        
        return services;
    }
}