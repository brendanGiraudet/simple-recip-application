using System.Linq.Expressions;
using Fluxor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using simple_recip_application.Components.OptionsMenu;
using simple_recip_application.Constants;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Factories;
using simple_recip_application.Features.RecipesManagement.Store;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipesManagement.UserInterfaces.Pages.RecipesPage;

public partial class RecipesPage
{
    [Inject] public required IState<RecipeState> RecipeState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IRecipeFactory RecipeFactory { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }
    [Inject] public required IAuthorizationService AuthorizationService { get; set; }
    [Inject] public required AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    private bool _canManageRecipe;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

        var authorizationResult = await AuthorizationService.AuthorizeAsync(authenticationState.User, FeatureFlagsConstants.RecipeManagementFeature);

        _canManageRecipe = authorizationResult.Succeeded;
    }

    private async Task OpenRecipFormModalAsync(IRecipeModel? model = null)
    {
        if (!_canManageRecipe) return;

        if (model is not null && model.Id.HasValue)
            Dispatcher.Dispatch(new LoadItemAction<IRecipeModel>(model.Id.Value));

        else
            Dispatcher.Dispatch(new SetItemAction<IRecipeModel>(RecipeFactory.Create()));

        Dispatcher.Dispatch(new SetFormModalVisibilityAction<IRecipeModel>(true));

        await Task.CompletedTask;
    }

    private void CloseRecipeFormModal(bool isUpdated) => Dispatcher.Dispatch(new SetFormModalVisibilityAction<IRecipeModel>(false));

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (RecipeState.Value.Items.Count() == 0)
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
        List<OptionMenuItem> options = [];

        if (_canManageRecipe)
            options.Add(new(MaterialIconsConstants.Add, string.Empty, () => OpenRecipFormModalAsync(), LabelsTranslator.AddRecipe));

        if (_canManageRecipe && RecipeState.Value.SelectedItems.Count() > 0)
            options.Add(new(MaterialIconsConstants.Delete, string.Empty, () => DeleteSelectedRecipesAsync(), LabelsTranslator.Delete));

        return options;
    }

    private async Task DeleteSelectedRecipesAsync()
    {
        if (RecipeState.Value.SelectedItems.Count() > 0)
        {
            Dispatcher.Dispatch(new DeleteItemsAction<IRecipeModel>(RecipeState.Value.SelectedItems));
        }

        await Task.CompletedTask;
    }

    private string _searchTerm = string.Empty;

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

    private bool CanPreviousClick() => RecipeState.Value.Skip > 0;
    private async Task OnPreviousAsync()
    {
        if (!CanPreviousClick()) return;

        var skip = RecipeState.Value.Skip - RecipeState.Value.Take;

        skip = skip < 0 ? 0 : skip;

        LoadFilteredRecipes(skip);

        await Task.CompletedTask;
    }

    private async Task OnNextAsync()
    {
        var skip = RecipeState.Value.Skip + RecipeState.Value.Take;

        LoadFilteredRecipes(skip);

        await Task.CompletedTask;
    }

    private void RedirectToDetails(Guid? recipeId)
    {
        if (_canManageRecipe)
            NavigationManager.NavigateTo(PageUrlsConstants.GetRecipeDetailsPage(recipeId));
    }
}