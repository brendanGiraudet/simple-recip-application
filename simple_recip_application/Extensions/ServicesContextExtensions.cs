using System.Net.Http.Headers;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using simple_recip_application.AuthorizationHandlers.FeatureFlagsAuthorizationHandler;
using simple_recip_application.Constants;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.IngredientsManagement.Persistence.Factories;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.NotificationsManagement.Persistence.Factories;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Factories;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Services;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Factories;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Services;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Factories;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Services;
using simple_recip_application.Features.RecipesManagement.Persistence.Factories;
using simple_recip_application.Features.RecipesManagement.Persistence.Services;
using simple_recip_application.Services;
using simple_recip_application.Settings;

namespace simple_recip_application.Extensions;

public static class ServicesContextExtensions
{
    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FileSettings>(configuration.GetSection(nameof(FileSettings)));
        services.Configure<OpenApisettings>(configuration.GetSection(nameof(OpenApisettings)));

        return services;
    }

    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        OpenApisettings openApisettings = new();
        configuration.GetSection(nameof(OpenApisettings)).Bind(openApisettings);

        services.AddHttpClient(HttpClientNamesConstants.OpenApi, options =>
        {
            options.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openApisettings.ApiKey);
        });

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IShoppingListGeneratorService, ShoppingListGeneratorService>();
        services.AddTransient<IOpenAiDataAnalysisService, OpenAiDataAnalysisService>();
        services.AddTransient<IRecipePlanifierService, RecipePlanifierService>();

        return services;
    }


    public static IServiceCollection AddFactories(this IServiceCollection services)
    {
        services.AddTransient<IRecipeIngredientFactory, RecipeIngredientFactory>();
        services.AddTransient<IRecipeFactory, RecipeFactory>();
        services.AddTransient<IIngredientFactory, IngredientFactory>();
        services.AddTransient<INotificationMessageFactory, NotificationMessageFactory>();
        services.AddTransient<IImportModelFactory, ImportModelFactory>();
        services.AddTransient<IPlanifiedRecipeModelFactory, PlanifiedRecipeModelFactory>();

        return services;
    }

    public static IServiceCollection AddAuthorizations(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, FeatureFlagsAuthorizationHandler>();

        services.AddCascadingAuthenticationState();

        services.AddAuthorization(options =>
        {
            foreach (var field in typeof(FeatureFlagsConstants).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var featureFlag = field.GetValue(null)?.ToString();

                if (!string.IsNullOrEmpty(featureFlag))
                {
                    options.AddPolicy(featureFlag, policy =>
                        policy.Requirements.Add(new FeatureFlagsAuthorizationRequirement(featureFlag)));
                }
            }
        });

        return services;
    }
}