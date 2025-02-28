using System.ComponentModel.DataAnnotations;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore;

namespace simple_recip_application.Features.RecipesManagement.Persistence.Entites;

public class RecipeIngredient : IRecipeIngredient
{
    public Guid RecipeId { get; set; }
    public RecipeModel Recipe { get; set; } = default!;
    public IRecipeModel RecipeModel
    {
        get => Recipe;
        set => Recipe = (RecipeModel)value;
    }

    public Guid IngredientId { get; set; }
    public IngredientModel Ingredient { get; set; } = default!;
    public IIngredientModel IngredientModel
    {
        get => Ingredient;
        set => Ingredient = (IngredientModel)value;
    }

    [Required]
    public decimal Quantity { get; set; }
}