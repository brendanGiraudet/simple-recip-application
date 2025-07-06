using System.Text;
using simple_recip_application.Dtos;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Entities;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Factories;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Services;
using simple_recip_application.Resources;

namespace simple_recip_application.Features.ShoppingListManagement.Persistence.Services;

public class ShoppingListGeneratorService
(
    ILogger<ShoppingListGeneratorService> _logger,
    IShoppingListItemModelFactory _shoppingListItemModelFactory,
    IRecipeRepository _recipeRepository
)
: IShoppingListGeneratorService
{
    public Task<MethodResult<string>> GenerateShoppingListCsvContentAsync(IEnumerable<IRecipeModel> recipes)
    {
        try
        {
            var ingredients = recipes.SelectMany(recipe => recipe.IngredientModels)
                                     .GroupBy(c => c.IngredientModel.Name)
                                     .Select(c => new { Name = c.Key, Quantity = c.Sum(t => t.Quantity), MeasureUnit = c.FirstOrDefault()?.IngredientModel.MeasureUnit ?? string.Empty })
                                     .ToList();

            var separator = ";";

            var csvContent = new StringBuilder();
            csvContent.AppendLine($"{LabelsTranslator.IngredientName}{separator}{LabelsTranslator.Quantity}{separator}{LabelsTranslator.MeasureUnit}");

            foreach (var ingredient in ingredients)
            {
                csvContent.AppendLine($"{ingredient.Name}{separator}{ingredient.Quantity.ToString("0.##")}{separator}{ingredient.MeasureUnit}");
            }

            return Task.FromResult(new MethodResult<string>(true, csvContent.ToString()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, MessagesTranslator.GenerateShoppingListCsvContentErrorMessage);

            return Task.FromResult(new MethodResult<string>(false, string.Empty));
        }
    }

    public Task<MethodResult<IEnumerable<IShoppingListItemModel>>> GenerateShoppingListItemsAsync(IEnumerable<IRecipeModel> recipes, string userId)
    {
        try
        {
            IEnumerable<IRecipeModel> loadedRecipes = [];
            foreach (var recipe in recipes)
            {
                _recipeRepository.GetByIdAsync(recipe.Id).ContinueWith(task =>
                {
                    if (task.Result.Success)
                    {
                        loadedRecipes = loadedRecipes.Append(task.Result.Item);
                    }
                    else
                    {
                        _logger.LogWarning(LabelsTranslator.NoRecipesFound, recipe.Id);
                    }
                });
            }

            var ingredients = loadedRecipes.SelectMany(recipe => recipe.IngredientModels)
                                     .GroupBy(c => c.IngredientModel.Name)
                                     .Select(c => _shoppingListItemModelFactory.Create(userId, c.FirstOrDefault()?.IngredientModel!, c.Sum(t => t.Quantity), false))
                                     .ToList();

            return Task.FromResult(new MethodResult<IEnumerable<IShoppingListItemModel>>(true, ingredients));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, MessagesTranslator.GenerateShoppingListCsvContentErrorMessage);

            return Task.FromResult(new MethodResult<IEnumerable<IShoppingListItemModel>>(false, [], string.Empty));
        }
    }
}
