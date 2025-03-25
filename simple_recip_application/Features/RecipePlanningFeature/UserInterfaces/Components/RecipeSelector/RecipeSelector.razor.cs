using System.Linq.Expressions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.Store;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipePlanningFeature.UserInterfaces.Components.RecipeSelector;

public partial class RecipeSelector
{
    [Inject] public required IState<RecipeState> RecipeState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }

    [Parameter] public EventCallback<IRecipeModel> OnRecipeSelected { get; set; }

    private string _searchTerm = string.Empty;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _searchTerm = string.Empty;

        LoadFilteredRecipes();
    }

    private void OnSearchTermChanged(string searchTerm)
    {
        _searchTerm = searchTerm;
        LoadFilteredRecipes();
    }

    private void LoadFilteredRecipes(int? skip = null)
    {
        Expression<Func<IRecipeModel, bool>>? filter = r => r.RemoveDate == null;

        if (!string.IsNullOrEmpty(_searchTerm))
            filter = i => i.Name.ToLower().Contains(_searchTerm.ToLower()) && i.RemoveDate == null;

        Dispatcher.Dispatch(new LoadItemsAction<IRecipeModel>(Take: RecipeState.Value.Take, Skip: skip ?? 0, filter));
    }

    private string GetRecipesVisibilityCssClass() => !RecipeState.Value.IsLoading ? "" : "hidden";

    private bool CanPreviousClick() => RecipeState.Value.Skip > 0;
    private async Task OnPrevious()
    {
        if (!CanPreviousClick()) return;

        var skip = RecipeState.Value.Skip - RecipeState.Value.Take;

        skip = skip < 0 ? 0 : skip;

        LoadFilteredRecipes(skip);

        await Task.CompletedTask;
    }

    private async Task OnNext()
    {
        var skip = RecipeState.Value.Skip + RecipeState.Value.Take;

        LoadFilteredRecipes(skip);

        await Task.CompletedTask;
    }
}
