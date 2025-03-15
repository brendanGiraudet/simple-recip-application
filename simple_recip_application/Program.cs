using simple_recip_application.Components;
using simple_recip_application.Extensions;
using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAntiforgery();

// Ajout de la base de données via l’extension
builder.Services.AddApplicationDbContext(builder.Configuration);

// Ajout des services de l’application
builder.Services.AddSettings(builder.Configuration);

// Ajout des factories
builder.Services.AddFactories();

// Ajout des services
builder.Services.AddServices();

// Ajout des httclient
builder.Services.AddHttpClients(builder.Configuration);

// Ajout des repositories
builder.Services.AddApplicationRepositories();

// Ajout Fluxor
builder.Services.AddFluxor(options => {
    options.ScanAssemblies(typeof(Program).Assembly);
    options.UseReduxDevTools();
});

// Ajout de la gestion des flags
builder.Services.AddFeatureManagement();

// Activer la localisation et spécifier le dossier contenant les fichiers `.resx`
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Définir la langue par défaut et les langues supportées
app.UseRequestLocalization(options =>
{
    string[] supportedCultures = ["fr-FR", "en-US"];
    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});

// Appliquer les migrations automatiquement
app.Services.ApplyMigrations();

app.MapControllers();

app.Run();
