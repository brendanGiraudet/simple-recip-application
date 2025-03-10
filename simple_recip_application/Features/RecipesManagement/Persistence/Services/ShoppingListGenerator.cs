using System.Text;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Services;
using simple_recip_application.Resources;

namespace simple_recip_application.Features.RecipesManagement.Persistence.Services;

public class ShoppingListGenerator : IShoppingListGenerator
{
    public Task<string> GenerateShoppingListCsvContentAsync(IEnumerable<IRecipeModel> recipes)
    {
        var ingredients = recipes.SelectMany(recipe => recipe.IngredientModels).GroupBy(c => c.IngredientModel.Name).Select(c => new { Name = c.Key, Quantity = c.Sum(t => t.Quantity), MeasureUnit = c.First().IngredientModel.MeasureUnit }).ToList();

        var csvContent = new StringBuilder();
        csvContent.AppendLine($"{LabelsTranslator.IngredientName},{LabelsTranslator.Quantity},{LabelsTranslator.MeasureUnit},{LabelsTranslator.Done}");

        foreach (var ingredient in ingredients)
        {
            csvContent.AppendLine($"{ingredient.Name},{ingredient.Quantity},{ingredient.MeasureUnit},false");
        }

        var content = csvContent.ToString();
        var csvBytes = Encoding.UTF8.GetBytes(content);

        return Task.FromResult(Convert.ToBase64String(csvBytes));
    }
}
