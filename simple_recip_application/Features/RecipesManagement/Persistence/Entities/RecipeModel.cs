using System.ComponentModel.DataAnnotations;
using simple_recip_application.Data.Persistence.Entities;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.ValidationAttributes;
using simple_recip_application.Features.RecipesManagement.ApplicationCore;
using simple_recip_application.Resources;

namespace simple_recip_application.Features.RecipesManagement.Persistence.Entites;

public class RecipeModel : EntityBase, IRecipeModel
{
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    public ICollection<RecipeIngredient> Ingredients { get; set; } = [];
    public ICollection<IRecipeIngredient> IngredientModels
    {
        get => Ingredients.Cast<IRecipeIngredient>().ToList();
        set => Ingredients = value.Cast<RecipeIngredient>().ToList();
    }

    [MaxLength(3000)]
    public string Instructions { get; set; } = string.Empty;

    [Required]
    public TimeOnly PreparationTime { get; set; }

    [Required]
    public TimeOnly CookingTime { get; set; }

    [RequiredImage(ErrorMessageResourceName = nameof(Messages.ImageRequired), ErrorMessageResourceType = typeof(Messages))]
    public byte[] Image { get; set; } = Array.Empty<byte>();

    public string Category { get; set; } = string.Empty;

    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    public DateTime? ModificationDate { get; set; } = DateTime.UtcNow;

    public DateTime? RemoveDate { get; set; }
}
