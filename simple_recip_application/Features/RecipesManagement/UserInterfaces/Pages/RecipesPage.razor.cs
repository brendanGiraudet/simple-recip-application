using System.Linq.Expressions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Components.OptionsMenu;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Factories;
using simple_recip_application.Features.RecipesManagement.Store;
using simple_recip_application.Features.RecipesManagement.Store.Actions;
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

        LoadFilteredIngredients();
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
            new ("add", string.Empty, () => OpenRecipFormModalAsync())
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

        if (!string.IsNullOrEmpty(_searchTerm))
            filter = i => i.Name.ToLower().Contains(_searchTerm.ToLower());

        Expression<Func<IRecipeModel, object>>? include = i => i.IngredientModels;

        Dispatcher.Dispatch(new LoadItemsAction<IRecipeModel>(Take: RecipeState.Value.Take, Skip: 0, filter, include));
    }
}