using System.Linq.Expressions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore;
using simple_recip_application.Features.IngredientsManagement.Store;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Factories;
using simple_recip_application.Features.RecipesManagement.Store;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipesManagement.UserInterfaces.Components.RecipeIngredientSelector;

public partial class RecipeIngredientSelector
{
    [Inject] public required IState<RecipeState> RecipeState { get; set; }
    [Inject] public required IState<IngredientState> IngredientState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IRecipeIngredientFactory RecipeIngredientFactory { get; set; }

    private ICollection<IIngredientModel> FilteredIngredients => IngredientState.Value.Items.Where(c => RecipeState.Value.Item.IngredientModels.FirstOrDefault(p => p.IngredientId == c.Id) == null).ToList();

    // This will hold the ICollection of ingredients filtered by search term
    private async Task SearchIngredients(string searchTerm)
    {
        Expression<Func<IIngredientModel, bool>>? filter = null;

        if (!string.IsNullOrEmpty(searchTerm))
            filter = i => i.Name.ToLower().Contains(searchTerm.ToLower());

        Dispatcher.Dispatch(new LoadItemsAction<IIngredientModel>(Take: IngredientState.Value.Take, Skip: 0, filter));
    }

    // Adds an ingredient to the selected list with quantity
    private async Task AddIngredient(IIngredientModel ingredient)
    {
        if (!RecipeState.Value.Item.IngredientModels.Any(i => i.IngredientModel == ingredient))
        {
            var recipeIngredient = RecipeIngredientFactory.Create(RecipeState.Value.Item, ingredient, 1);

            IEnumerable<IRecipeIngredientModel> ingredients = [.. RecipeState.Value.Item.IngredientModels, recipeIngredient];

            RecipeState.Value.Item.IngredientModels = ingredients.ToList();

            Dispatcher.Dispatch(new SetItemAction<IRecipeModel>(RecipeState.Value.Item));
        }
    }

    // Removes an ingredient from the selected list
    private void RemoveIngredient(IRecipeIngredientModel ingredient)
    {
        RecipeState.Value.Item.IngredientModels.Remove(ingredient);

        Dispatcher.Dispatch(new SetItemAction<IRecipeModel>(RecipeState.Value.Item));
    }
}