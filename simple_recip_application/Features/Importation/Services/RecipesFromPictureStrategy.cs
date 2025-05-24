using Fluxor;
using simple_recip_application.Dtos;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Resources;
using simple_recip_application.Services;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.Importation.Services;

public class RecipesFromPictureStrategy
(
    IServiceProvider _serviceProvider,
    IDispatcher _dispatcher,
    IIngredientRepository _ingredientRepository
)
: IImportStrategy
{
    public async Task<MethodResult> ImportDataAsync(byte[] fileContent)
    {
        try
        {
            var _services = _serviceProvider.GetRequiredService<IOpenAiDataAnalysisService>();

            var recipeResult = await _services.ExtractRecipeFromImageAsync(fileContent);

            if (!recipeResult.Success || recipeResult.Item is null)
                return new MethodResult(false, MessagesTranslator.ImportFailure);

            var ingredients = recipeResult.Item.IngredientModels.Select(t => t.IngredientModel?.Name) ?? [];
            var existedIngredientsResult = await _ingredientRepository.GetAsync(recipeResult.Item.IngredientModels.Count(), 0, c => ingredients.Contains(c.Name));

            if (!existedIngredientsResult.Success)
                return new MethodResult(false, MessagesTranslator.ImportFailure);

            var defaultImagePath = "./wwwroot/images/ingredient_default_image.webp";

            var defaultImage = await File.ReadAllBytesAsync(defaultImagePath);

            foreach (var ingredient in recipeResult.Item.IngredientModels)
            {
                var existedIngredient = existedIngredientsResult.Item.FirstOrDefault(c => string.Equals(c.Name, ingredient.IngredientModel?.Name, StringComparison.InvariantCultureIgnoreCase));

                if (existedIngredient is not null)
                    ingredient.IngredientModel = existedIngredient;

                else
                    ingredient.IngredientModel.Image = defaultImage;
            }

            recipeResult.Item.Image = defaultImage;

            _dispatcher.Dispatch(new AddItemAction<IRecipeModel>(recipeResult.Item!));

            return new MethodResult(true, MessagesTranslator.ImportSuccess);
        }
        catch (Exception ex)
        {
            return new MethodResult(false, MessagesTranslator.ImportFailure);
        }
    }
}
