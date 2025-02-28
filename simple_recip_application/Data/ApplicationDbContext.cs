using Microsoft.EntityFrameworkCore;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;

namespace simple_recip_application.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<IngredientModel> Ingredients => Set<IngredientModel>();
    public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();
    public DbSet<RecipeModel> Recipes => Set<RecipeModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IngredientModel>(entity =>
        {
            entity.HasKey(i => i.Id);

            entity.Property(i => i.Name).IsRequired().HasMaxLength(255);

            entity.Property(i => i.Image).HasColumnType("BLOB").IsRequired();

            entity.Property(i => i.CreationDate).IsRequired();

            entity.Property(i => i.ModificationDate);

            entity.Property(i => i.RemoveDate);
        });


        modelBuilder.Entity<RecipeModel>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.Property(r => r.Name).IsRequired().HasMaxLength(255);

            entity.Property(r => r.Description).HasMaxLength(1000);

            entity.Property(r => r.Instructions).IsRequired().HasMaxLength(3000);

            entity.Property(r => r.PreparationTime).IsRequired();

            entity.Property(r => r.CookingTime).IsRequired();

            entity.Property(i => i.Image).HasColumnType("BLOB").IsRequired();

            entity.Property(r => r.Category).IsRequired();

            entity.Property(r => r.CreationDate).IsRequired();

            entity.Property(r => r.ModificationDate);

            entity.Property(r => r.RemoveDate);

            entity.Ignore(r => r.IngredientModels);
        });

        modelBuilder.Entity<RecipeIngredient>(entity =>
        {
            entity.HasKey(ri => new { ri.RecipeId, ri.IngredientId });

            entity.HasOne(ri => ri.Recipe)
                  .WithMany(r => r.Ingredients)
                  .HasForeignKey(ri => ri.RecipeId);

            entity.HasOne(ri => ri.Ingredient)
                  .WithMany()
                  .HasForeignKey(ri => ri.IngredientId);

            entity.Property(r => r.Quantity).IsRequired();

            entity.Ignore(ri => ri.RecipeModel);

            entity.Ignore(ri => ri.IngredientModel);
        });
    }
}