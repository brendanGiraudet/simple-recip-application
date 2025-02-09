using Fluxor;
using simple_recip_application.Components;
using simple_recip_application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Ajout de la base de données via l’extension
builder.Services.AddApplicationDbContext(builder.Configuration);

// Ajout des repositories
builder.Services.AddApplicationRepositories();

// Ajout Fluxor
builder.Services.AddFluxor(options => options.ScanAssemblies(typeof(Program).Assembly));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Appliquer les migrations automatiquement
app.Services.ApplyMigrations();

app.Run();
