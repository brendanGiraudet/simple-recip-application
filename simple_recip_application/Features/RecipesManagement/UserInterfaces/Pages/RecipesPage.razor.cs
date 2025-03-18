using System.Linq.Expressions;
using Fluxor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using simple_recip_application.Components.OptionsMenu;
using simple_recip_application.Constants;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Factories;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Services;
using simple_recip_application.Features.RecipesManagement.Store;
using simple_recip_application.Features.RecipesManagement.Store.Actions;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipesManagement.UserInterfaces.Pages;

public partial class RecipesPage
{
    [Inject] public required ILogger<RecipesPage> Logger { get; set; }
    [Inject] public required IState<RecipeState> RecipeState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IRecipeFactory RecipeFactory { get; set; }
    [Inject] public required IJSRuntime JSRuntime { get; set; }
    [Inject] public required IShoppingListGenerator ShoppingListGenerator { get; set; }
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

    private IJSObjectReference? _module;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
            _module = await JSRuntime.InvokeAsync<IJSObjectReference>("import",
            "./js/download.js");
    }

    private async Task OpenRecipFormModalAsync(IRecipeModel? model = null)
    {
        if (!_canManageRecipe) return;

        if (model is not null && model.Id.HasValue)
            Dispatcher.Dispatch(new LoadItemAction<IRecipeModel>(model.Id.Value));

        else
            Dispatcher.Dispatch(new SetItemAction<IRecipeModel>(RecipeFactory.Create()));

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
            new ("shopping_cart_checkout", string.Empty, () => GenerateCsvAsync(), LabelsTranslator.GenerateShoppingList),
        ];

        if (_canManageRecipe)
            options.Add(new("add", string.Empty, () => OpenRecipFormModalAsync(), LabelsTranslator.AddRecipe));

        return options;
    }

    private async Task GenerateCsvAsync()
    {
        try
        {
            var content = await ShoppingListGenerator.GenerateShoppingListCsvContentAsync(RecipeState.Value.SelectedItems);

            var csvDataUrl = $"data:text/csv;base64,{content}";

            var filename = "ingredients.csv";

            if (_module is not null)
                await _module.InvokeVoidAsync("triggerFileDownload", filename, csvDataUrl);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"Error during GenerateCsvAsync: {ex.Message}");
        }
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

    private void RedirectToDetails(Guid? recipeId)
    {
        if(_canManageRecipe)
            NavigationManager.NavigateTo(PageUrlsConstants.GetRecipeDetailsPage(recipeId));
    }
}