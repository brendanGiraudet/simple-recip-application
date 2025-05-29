using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;
using simple_recip_application.Data;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;
using simple_recip_application.Features.RecipesManagement.Persistence.Repositories;
using simple_recip_application.Tests.Fakers;

public class RecipeRepositoryBenchmark
{
    private ApplicationDbContext _context = default!;
    private IRecipeRepository _recipeRepository = default!;

    [GlobalSetup]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "BenchmarkDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        SeedData(_context);

        _recipeRepository = new RecipeRepository(_context);
    }

    private void SeedData(ApplicationDbContext context)
    {
        var faker = new RecipeModelFaker();
        var recipes = faker.Generate(1000);  // Génère 1000 recettes

        context.Set<RecipeModel>().AddRange(recipes.Cast<RecipeModel>());
        context.SaveChanges();
    }

    [Benchmark]
    public async Task GetRecipesAsync()
    {
        var result = await _recipeRepository.GetAsync(20, 480, null);

        if (!result.Success) throw new Exception("Erreur récupération page intermédiaire");
    }
}
