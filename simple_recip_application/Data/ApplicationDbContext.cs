using Microsoft.EntityFrameworkCore;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;

namespace simple_recip_application.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSet basé sur l'interface pour abstraction métier
    public DbSet<IngredientModel> Ingredients => Set<IngredientModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuration de l'implémentation concrète IngredientModel
        modelBuilder.Entity<IngredientModel>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.Property(i => i.Name).IsRequired().HasMaxLength(255);
            entity.Property(i => i.Image).HasColumnType("BLOB");
            entity.Property(i => i.CreationDate).IsRequired();
            entity.Property(i => i.ModificationDate);
            entity.Property(i => i.RemoveDate);
        });
    }
}