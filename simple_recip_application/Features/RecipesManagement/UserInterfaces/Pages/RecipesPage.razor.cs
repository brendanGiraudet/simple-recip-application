using System.Linq.Expressions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Components.OptionsMenu;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Factories;
using simple_recip_application.Features.RecipesManagement.Store;
using simple_recip_application.Features.RecipesManagement.Store.Actions;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipesManagement.UserInterfaces.Pages;

public partial class RecipesPage
{
    [Inject] public required IState<RecipeState> RecipeState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IRecipeFactory RecipeFactory { get; set; }

    private async Task OpenRecipFormModalAsync(IRecipeModel? model = null)
    {
        Dispatcher.Dispatch(new SetItemAction<IRecipeModel>(model ?? RecipeFactory.Create()));

        Dispatcher.Dispatch(new SetRecipeFormModalVisibilityAction(true));

        await Task.CompletedTask;
    }

    private void CloseRecipeFormModal(bool isUpdated) => Dispatcher.Dispatch(new SetRecipeFormModalVisibilityAction(false));

    protected override void OnInitialized()
    {
        base.OnInitialized();

        LoadFilteredRecipes();
    }

    private string GetRecipesVisibilityCssClass() => !RecipeState.Value.IsLoading ? "" : "hidden";

    private void HandleSelection(IRecipeModel Recipe)
    {
        if (RecipeState.Value.SelectedItems.Contains(Recipe))
            RecipeState.Value.SelectedItems = RecipeState.Value.SelectedItems.Except([Recipe]);

        else
            RecipeState.Value.SelectedItems = RecipeState.Value.SelectedItems.Append(Recipe);
    }

    private List<OptionMenuItem> GetOptions()
    {
        List<OptionMenuItem> options = [
            new ("add", string.Empty, () => OpenRecipFormModalAsync(), LabelsTranslator.AddRecipe),
        ];

        return options;
    }

    private string _searchTerm = string.Empty;

    private void OnSearchTermChanged(string searchTerm)
    {
        _searchTerm = searchTerm;
        LoadFilteredRecipes();
    }

    private void LoadFilteredRecipes(int? skip = null)
    {
        Expression<Func<IRecipeModel, bool>>? filter = null;

        if (!string.IsNullOrEmpty(_searchTerm))
            filter = i => i.Name.ToLower().Contains(_searchTerm.ToLower());

        Expression<Func<IRecipeModel, object>>? include = i => i.IngredientModels;

        Dispatcher.Dispatch(new LoadItemsAction<IRecipeModel>(Take: RecipeState.Value.Take, Skip: skip ?? 0, filter, include));
    }

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