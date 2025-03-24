using System.Globalization;
using System.Text;
using simple_recip_application.Dtos;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Services;
using simple_recip_application.Resources;

namespace simple_recip_application.Features.RecipesManagement.Persistence.Services;

public class ShoppingListGeneratorService
(
    ILogger<ShoppingListGeneratorService> _logger
)
: IShoppingListGeneratorService
{
    public Task<MethodResult<string>> GenerateShoppingListCsvContentAsync(IEnumerable<IRecipeModel> recipes)
    {
        try
        {
            var ingredients = recipes.SelectMany(recipe => recipe.IngredientModels).GroupBy(c => c.IngredientModel.Name).Select(c => new { Name = c.Key, Quantity = c.Sum(t => t.Quantity), MeasureUnit = c.FirstOrDefault()?.IngredientModel.MeasureUnit ?? string.Empty }).ToList();

            var separator = ";";

            var csvContent = new StringBuilder();
            csvContent.AppendLine($"{LabelsTranslator.IngredientName}{separator}{LabelsTranslator.Quantity}{separator}{LabelsTranslator.MeasureUnit}");

            foreach (var ingredient in ingredients)
            {
                csvContent.AppendLine($"{ingredient.Name}{separator}{ingredient.Quantity.ToString("0.##")}{separator}{ingredient.MeasureUnit}");
            }

            return Task.FromResult(new MethodResult<string>(true, csvContent.ToString()));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, MessagesTranslator.GenerateShoppingListCsvContentErrorMessage);

            return Task.FromResult(new MethodResult<string>(false, string.Empty));
        }
    }
}
