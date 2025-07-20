using Microsoft.EntityFrameworkCore;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;
using simple_recip_application.Features.HouseholdProductsManagement.Persistence.Entities;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Entities;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;
using simple_recip_application.Features.UserPantryManagement.Persistence.Entities;
using simple_recip_application.Features.TagsManagement.Persistence.Entities;
using simple_recip_application.Features.ShoppingListManagement.Persistence.Entities;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Entites;

namespace simple_recip_application.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<IngredientModel> Ingredients => Set<IngredientModel>();
    public DbSet<RecipeIngredientModel> RecipeIngredients => Set<RecipeIngredientModel>();
    public DbSet<RecipeTagModel> RecipeTagModels => Set<RecipeTagModel>();
    public DbSet<RecipeModel> Recipes => Set<RecipeModel>();
    public DbSet<PlanifiedRecipeModel> PlanifiedRecipes => Set<PlanifiedRecipeModel>();
    public DbSet<HouseholdProductModel> HouseholdProducts => Set<HouseholdProductModel>();
    public DbSet<UserPantryItemModel> UserPantryItems => Set<UserPantryItemModel>();
    public DbSet<TagModel> Tags => Set<TagModel>();
    public DbSet<ShoppingListItemModel> ShoppingListItems => Set<ShoppingListItemModel>();
    public DbSet<CalendarModel> CalendarModels => Set<CalendarModel>();
    public DbSet<CalendarUserAccessModel> CalendarUserAccessModels => Set<CalendarUserAccessModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserPantryItemModel>(entity =>
        {
            entity.HasKey(ri => new { ri.UserId, ri.ProductId });

            entity.Property(i => i.Quantity).IsRequired();
            
            entity.Ignore(i => i.ProductModel);
        });
        
        modelBuilder.Entity<ShoppingListItemModel>(entity =>
        {
            entity.HasKey(ri => new { ri.UserId, ri.ProductId });
            
            entity.Ignore(i => i.ProductModel);
        });
        
        modelBuilder.Entity<TagModel>(entity =>
        {
            entity.HasKey(i => i.Id);

            entity.Property(i => i.Name).IsRequired().HasMaxLength(50);

            entity.Property(i => i.CreationDate).IsRequired();

            entity.Property(i => i.ModificationDate);

            entity.Property(i => i.RemoveDate);
        });
        
        modelBuilder.Entity<IngredientModel>(entity =>
        {
            entity.HasKey(i => i.Id);

            entity.Property(i => i.Name).IsRequired().HasMaxLength(255);

            entity.Property(i => i.Image).HasColumnType("BLOB").IsRequired();

            entity.Property(r => r.MeasureUnit).IsRequired().HasMaxLength(50);

            entity.Property(i => i.CreationDate).IsRequired();

            entity.Property(i => i.ModificationDate);

            entity.Property(i => i.RemoveDate);
        });

        modelBuilder.Entity<HouseholdProductModel>(entity =>
        {
            entity.HasKey(i => i.Id);

            entity.Property(i => i.Name).IsRequired().HasMaxLength(255);

            entity.Property(i => i.Image).HasColumnType("BLOB").IsRequired();

            entity.Property(r => r.MeasureUnit).IsRequired().HasMaxLength(50);

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
            
            entity.Ignore(r => r.TagModels);
        });

        modelBuilder.Entity<RecipeIngredientModel>(entity =>
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
        
        modelBuilder.Entity<RecipeTagModel>(entity =>
        {
            entity.HasKey(ri => new { ri.RecipeId, ri.TagId });

            entity.HasOne(ri => ri.Recipe)
                  .WithMany(r => r.Tags)
                  .HasForeignKey(ri => ri.RecipeId);

            entity.HasOne(ri => ri.Tag)
                  .WithMany()
                  .HasForeignKey(ri => ri.TagId);

            entity.Ignore(ri => ri.RecipeModel);

            entity.Ignore(ri => ri.TagModel);
        });

        modelBuilder.Entity<PlanifiedRecipeModel>(entity =>
        {
            entity.HasKey(r => new { r.RecipeId, r.PlanifiedDateTime, r.CalendarId });

            entity.HasOne(r => r.Recipe)
                  .WithMany()
                  .HasForeignKey(r => r.RecipeId);

            entity.Ignore(r => r.RecipeModel);

            entity.Property(r => r.PlanifiedDateTime).IsRequired();

            entity.HasOne(r => r.Calendar)
                  .WithMany()
                  .HasForeignKey(r => r.CalendarId);

            entity.Ignore(r => r.CalendarModel);

            entity.Property(r => r.MomentOftheDay);
        });

        modelBuilder.Entity<CalendarModel>(entity =>
        {
            entity.HasKey(c => c.Id);
            
            entity.Property(c => c.Name).IsRequired().HasMaxLength(255);
            
            entity.Property(c => c.CreationDate).IsRequired();
            
            entity.Property(c => c.ModificationDate);
            
            entity.Property(c => c.RemoveDate);

            entity.HasMany(c => c.PlanifiedRecipes)
                  .WithOne(r => r.Calendar)
                  .HasForeignKey(r => r.CalendarId);

            entity.Ignore(c => c.PlanifiedRecipeModels);

            entity.HasMany(c => c.CalendarUsersAccess)
                    .WithOne(cua => cua.Calendar)
                    .HasForeignKey(cua => cua.CalendarId);

            entity.Ignore(c => c.CalendarUserAccessModels);
        });

        modelBuilder.Entity<CalendarUserAccessModel>(entity =>
        {
            entity.HasKey(cua => new { cua.CalendarId, cua.UserId });
            
            entity.HasOne(cua => cua.Calendar)
                  .WithMany(c => c.CalendarUsersAccess)
                  .HasForeignKey(cua => cua.CalendarId);

            entity.Ignore(entity => entity.CalendarModel);
        });
    }
}