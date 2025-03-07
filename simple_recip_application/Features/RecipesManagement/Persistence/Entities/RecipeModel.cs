using System.ComponentModel.DataAnnotations;
using simple_recip_application.Data.ApplicationCore.ValidationAttributes;
using simple_recip_application.Data.Persistence.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Resources;

namespace simple_recip_application.Features.RecipesManagement.Persistence.Entites;

public class RecipeModel : EntityBase, IRecipeModel
{
    [Required(ErrorMessageResourceName = nameof(MessagesTranslator.NameRequired), ErrorMessageResourceType = typeof(MessagesTranslator))]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    public ICollection<RecipeIngredientModel> Ingredients { get; set; } = [];
    public ICollection<IRecipeIngredientModel> IngredientModels
    {
        get => Ingredients.Cast<IRecipeIngredientModel>().ToList();
        set => Ingredients = value.Cast<RecipeIngredientModel>().ToList();
    }

    [MaxLength(3000)]
    public string Instructions { get; set; } = string.Empty;

    [Required(ErrorMessageResourceName = nameof(MessagesTranslator.PreparationTimeRequired), ErrorMessageResourceType = typeof(MessagesTranslator))]
    public TimeOnly PreparationTime { get; set; }

    [Required(ErrorMessageResourceName = nameof(MessagesTranslator.CookingTimeRequired), ErrorMessageResourceType = typeof(MessagesTranslator))]
    public TimeOnly CookingTime { get; set; }

    [RequiredImage(ErrorMessageResourceName = nameof(MessagesTranslator.ImageRequired), ErrorMessageResourceType = typeof(MessagesTranslator))]
    public byte[] Image { get; set; } = Array.Empty<byte>();

    public string Category { get; set; } = string.Empty;

    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    public DateTime? ModificationDate { get; set; } = DateTime.UtcNow;

    public DateTime? RemoveDate { get; set; }
}
