using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Constants;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.RecipesManagement.Store.Actions;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipesManagement.Store;

public class RecipeEffects
(
    IRecipeRepository _repository,
    ILogger<RecipeEffects> _logger,
    NavigationManager _navigationManager
)
{
    [EffectMethod]
    public async Task HandleLoadRecipes(LoadItemsAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        try
        {
            var recipesResult = await _repository.GetAsync(action.Take, action.Skip, action.Predicate);

            if (!recipesResult.Success)
                dispatcher.Dispatch(new LoadItemsFailureAction<IRecipeModel>());

            else
                dispatcher.Dispatch(new LoadItemsSuccessAction<IRecipeModel>(recipesResult.Item!));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors du chargement des ingrédients");

            dispatcher.Dispatch(new LoadItemsFailureAction<IRecipeModel>());
        }
    }

    [EffectMethod]
    public async Task HandleLoadRecipe(LoadItemAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        try
        {
            var recipeResult = await _repository.GetByIdAsync(action.Id);

            if (!recipeResult.Success)
                dispatcher.Dispatch(new LoadItemFailureAction<IRecipeModel>());

            else
                dispatcher.Dispatch(new LoadItemSuccessAction<IRecipeModel>(recipeResult.Item));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors du chargement de la recette");

            dispatcher.Dispatch(new LoadItemFailureAction<IRecipeModel>());
        }
    }

    [EffectMethod]
    public async Task HandleAddRecipe(AddItemAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        try
        {
            var addResult = await _repository.AddAsync(action.Item);

            if (!addResult.Success)
                dispatcher.Dispatch(new AddItemFailureAction<IRecipeModel>(action.Item));

            else
            {
                dispatcher.Dispatch(new AddItemSuccessAction<IRecipeModel>(action.Item));
                dispatcher.Dispatch(new SetRecipeFormModalVisibilityAction(false));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de l'ajout d'un ingrédient");

            dispatcher.Dispatch(new AddItemFailureAction<IRecipeModel>(action.Item));
        }
    }

    [EffectMethod]
    public async Task HandleDeleteRecipe(DeleteItemSuccessAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        _navigationManager.NavigateTo(PageUrlsConstants.RecipesPage);
    }

    [EffectMethod]
    public async Task HandleDeleteRecipe(DeleteItemAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        try
        {
            if (!action.Item.Id.HasValue)
            {
                dispatcher.Dispatch(new DeleteItemFailureAction<IRecipeModel>(action.Item));

                return;
            }

            var recipeResult = await _repository.GetByIdAsync(action.Item.Id.Value);
            if (!recipeResult.Success || recipeResult.Item == null)
            {
                dispatcher.Dispatch(new DeleteItemFailureAction<IRecipeModel>(action.Item));

                return;
            }

            var deleteResult = await _repository.DeleteAsync(recipeResult.Item);

            if (deleteResult.Success)
                dispatcher.Dispatch(new DeleteItemFailureAction<IRecipeModel>(action.Item));

            else
            {
                dispatcher.Dispatch(new DeleteItemSuccessAction<IRecipeModel>(action.Item));
                dispatcher.Dispatch(new SetRecipeFormModalVisibilityAction(false));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la suppression de la recette");

            dispatcher.Dispatch(new DeleteItemFailureAction<IRecipeModel>(action.Item));
        }
    }

    [EffectMethod]
    public async Task HandleUpdateRecipe(UpdateItemAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        try
        {
            var updateResult = await _repository.UpdateAsync(action.Item);

            if (!updateResult.Success)
                dispatcher.Dispatch(new UpdateItemFailureAction<IRecipeModel>(action.Item));

            else
            {
                dispatcher.Dispatch(new UpdateItemSuccessAction<IRecipeModel>(action.Item));
                dispatcher.Dispatch(new SetRecipeFormModalVisibilityAction(false));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la mise à jour");

            dispatcher.Dispatch(new UpdateItemFailureAction<IRecipeModel>(action.Item));
        }
    }
}
