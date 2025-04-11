using simple_recip_application.Features.TagsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.TagsManagement.Persistence.Factories;
using simple_recip_application.Features.TagsManagement.Persistence.Repositories;

namespace simple_recip_application.Extensions;

public static class TagServicesContextExtensions
{
    public static IServiceCollection AddTagFeatureDependencies(this IServiceCollection services)
    {
        services.AddTransient<ITagFactory, TagFactory>();
        services.AddTransient<ITagRepository, TagRepository>();

        return services;
    }
}