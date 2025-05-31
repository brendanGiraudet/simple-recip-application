using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Constants;
using simple_recip_application.Dtos;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Repositories;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipesManagement.Store;

public class RecipeEffects
(
    ILogger<RecipeEffects> _logger,
    NavigationManager _navigationManager,
    IServiceScopeFactory ScopeFactory
)
{
    [EffectMethod]
    public async Task HandleLoadRecipes(LoadItemsAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        try
        {
            await Task.Run(async () =>
            {
                using var scope = ScopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IRecipeRepository>();

                var recipesResult = await repository.GetAsync(action.Take, action.Skip, action.Predicate);

                if (!recipesResult.Success)
                    dispatcher.Dispatch(new LoadItemsFailureAction<IRecipeModel>());

                else
                    dispatcher.Dispatch(new LoadItemsSuccessAction<IRecipeModel>(recipesResult.Item!));
            });
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
            await Task.Run(async () =>
            {
                using var scope = ScopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IRecipeRepository>();

                var recipeResult = await repository.GetByIdAsync(action.Id);

                if (!recipeResult.Success)
                    dispatcher.Dispatch(new LoadItemFailureAction<IRecipeModel>());

                else
                    dispatcher.Dispatch(new LoadItemSuccessAction<IRecipeModel>(recipeResult.Item));
            });
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
            await Task.Run(async () =>
            {
                using var scope = ScopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IRecipeRepository>();

                var addResult = await repository.AddAsync(action.Item);

                if (!addResult.Success)
                    dispatcher.Dispatch(new AddItemFailureAction<IRecipeModel>(action.Item));

                else
                {
                    dispatcher.Dispatch(new AddItemSuccessAction<IRecipeModel>(action.Item));
                    dispatcher.Dispatch(new SetFormModalVisibilityAction<IRecipeModel>(false));
                }
            });
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
        var deleteResult = await DeleteRecipe(action.Item);

        if (!deleteResult.Success)
            dispatcher.Dispatch(new DeleteItemFailureAction<IRecipeModel>(action.Item));

        else
        {
            dispatcher.Dispatch(new DeleteItemSuccessAction<IRecipeModel>(action.Item));
            dispatcher.Dispatch(new SetFormModalVisibilityAction<IRecipeModel>(false));
        }
    }

    private async Task<MethodResult> DeleteRecipe(IRecipeModel recipe)
    {
        try
        {
            return await Task.Run(async () =>
            {
                using var scope = ScopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IRecipeRepository>();

                if (!recipe.Id.HasValue)
                    return new MethodResult(false);

                var recipeResult = await repository.GetByIdAsync(recipe.Id.Value);
                if (!recipeResult.Success || recipeResult.Item == null)
                    return new MethodResult(false);

                var deleteResult = await repository.DeleteAsync(recipeResult.Item);

                return new MethodResult(deleteResult.Success);
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la suppression de la recette");

            return new MethodResult(false);
        }
    }

    [EffectMethod]
    public async Task HandleDeleteRecipes(DeleteItemsAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        var deleteSuccess = true;
        foreach (var recipe in action.Items)
        {
            var deleteResult = await DeleteRecipe(recipe);

            deleteSuccess &= deleteResult.Success;
        }

        if (!deleteSuccess)
            dispatcher.Dispatch(new DeleteItemsFailureAction<IRecipeModel>(action.Items));
        else
        {
            dispatcher.Dispatch(new DeleteItemsSuccessAction<IRecipeModel>(action.Items));
        }
    }

    [EffectMethod]
    public async Task HandleUpdateRecipe(UpdateItemAction<IRecipeModel> action, IDispatcher dispatcher)
    {
        try
        {
            await Task.Run(async () =>
            {
                using var scope = ScopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IRecipeRepository>();

                var updateResult = await repository.UpdateAsync(action.Item);

                if (!updateResult.Success)
                    dispatcher.Dispatch(new UpdateItemFailureAction<IRecipeModel>(action.Item));

                else
                {
                    dispatcher.Dispatch(new UpdateItemSuccessAction<IRecipeModel>(action.Item));
                    dispatcher.Dispatch(new SetFormModalVisibilityAction<IRecipeModel>(false));
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la mise à jour");

            dispatcher.Dispatch(new UpdateItemFailureAction<IRecipeModel>(action.Item));
        }
    }
}
