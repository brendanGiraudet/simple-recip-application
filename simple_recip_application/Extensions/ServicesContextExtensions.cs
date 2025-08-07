using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using simple_recip_application.AuthorizationHandlers.FeatureFlagsAuthorizationHandler;
using simple_recip_application.Constants;
using simple_recip_application.Features.UserInfos.ApplicationCore.AuthenticationStateProvider;
using simple_recip_application.Services;
using simple_recip_application.Services.EmailService;
using simple_recip_application.Settings;
using System.Net.Http.Headers;
using System.Reflection;

namespace simple_recip_application.Extensions;

public static class ServicesContextExtensions
{
    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FileSettings>(configuration.GetSection(nameof(FileSettings)));
        services.Configure<OpenApisettings>(configuration.GetSection(nameof(OpenApisettings)));
        services.Configure<OAuthGoogleSettings>(configuration.GetSection(nameof(OAuthGoogleSettings)));
        services.Configure<SimpleRecipeApplicationSettings>(configuration.GetSection(nameof(SimpleRecipeApplicationSettings)));

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

    public static IServiceCollection AddSharedServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IOpenAiDataAnalysisService, OpenAiDataAnalysisService>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

        MailtrapSettings mailtrapSettings = new MailtrapSettings();
        configuration.GetSection(nameof(MailtrapSettings)).Bind(mailtrapSettings);

        services.AddFluentEmail(mailtrapSettings.Email)
                .AddRazorRenderer()
                .AddMailtrapSender(mailtrapSettings.Username, mailtrapSettings.Password, mailtrapSettings.Host, mailtrapSettings.Port);

        services.AddTransient<HtmlRenderer>();

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

    public static IServiceCollection AddAuthentications(this IServiceCollection services, IConfiguration configuration)
    {
        var oAuthGoogleSettings = new OAuthGoogleSettings();
        configuration.GetSection(nameof(OAuthGoogleSettings)).Bind(oAuthGoogleSettings);

        // Authentification Google
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            options.Cookie.Name = "SimpleRecipApplication";
        })
        .AddGoogle(options =>
        {
            options.ClientId = oAuthGoogleSettings.ClientId;
            options.ClientSecret = oAuthGoogleSettings.ClientSecret;
        });

        return services;
    }
}