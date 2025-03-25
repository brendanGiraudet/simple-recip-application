using Microsoft.EntityFrameworkCore;
using simple_recip_application.Data;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.IngredientsManagement.Persistence.Repositories;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Repositories;
using simple_recip_application.Features.RecipesManagement.Persistence.Repositories;

namespace simple_recip_application.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));

        return services;
    }

    public static void ApplyMigrations(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    }

    public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
    {
        services.AddScoped<IIngredientRepository, IngredientRepository>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<IPlanifiedRecipeRepository, PlanifiedRecipeRepository>();
        
        return services;
    }
}
