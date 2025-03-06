using System.Linq.Expressions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Components.OptionsMenu;
using simple_recip_application.Features.RecipesManagement.ApplicationCore;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;
using simple_recip_application.Features.RecipesManagement.Store;
using simple_recip_application.Features.RecipesManagement.Store.Actions;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipesManagement.UserInterfaces.Pages;

public partial class RecipesPage
{
    [Inject] public required IState<RecipeState> RecipeState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }

    private IRecipeModel? _selectedRecipe { get; set; } = new RecipeModel();

    private async Task OpenRecipFormModalAsync(IRecipeModel? model = null)
    {
        _selectedRecipe = model ?? new RecipeModel();

        Dispatcher.Dispatch(new SetRecipeFormModalVisibilityAction(true));

        await Task.CompletedTask;
    }

    private void CloseRecipeFormModal(bool isUpdated) => Dispatcher.Dispatch(new SetRecipeFormModalVisibilityAction(false));

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Dispatcher.Dispatch(new LoadItemsAction<IRecipeModel>());
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
            new ("add", Labels.AddRecipe, () => OpenRecipFormModalAsync())
        ];

        return options;
    }

    private string _searchTerm = string.Empty;

    private void OnSearchTermChanged(string searchTerm)
    {
        _searchTerm = searchTerm;
        LoadFilteredIngredients();
    }

    private void LoadFilteredIngredients()
    {
        Expression<Func<IRecipeModel, bool>>? filter = null;

        if(!string.IsNullOrEmpty(_searchTerm))
            filter = i => i.Name.ToLower().Contains(_searchTerm.ToLower());

        Dispatcher.Dispatch(new LoadItemsAction<IRecipeModel>(Take: RecipeState.Value.Take, Skip: 0, filter));
    }
}