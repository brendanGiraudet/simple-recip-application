using System.Linq.Expressions;
using Fluxor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using simple_recip_application.Constants;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.IngredientsManagement.Store;
using simple_recip_application.Features.IngredientsManagement.Store.Actions;
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
    [Inject] public required IIngredientFactory IngredientFactory { get; set; }
    [Inject] public required IAuthorizationService AuthorizationService { get; set; }
    [Inject] public required AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    private ICollection<IRecipeIngredientModel> RecipeIngredients => RecipeState.Value.Item?.IngredientModels ?? [];

    private async Task SearchIngredients(string? searchTerm = null)
    {
        Expression<Func<IIngredientModel, bool>>? filter = null;

        if (!string.IsNullOrEmpty(searchTerm))
            filter = i => i.Name.ToLower().Contains(searchTerm.ToLower());

        Dispatcher.Dispatch(new LoadItemsAction<IIngredientModel>(Take: IngredientState.Value.Take, Skip: 0, filter));

        await Task.CompletedTask;
    }

    private async Task AddIngredient(IIngredientModel ingredient)
    {
        if (!RecipeState.Value.Item.IngredientModels.Any(i => i.IngredientModel == ingredient))
        {
            var recipeIngredient = RecipeIngredientFactory.Create(RecipeState.Value.Item, ingredient, 1);

            RecipeState.Value.Item.IngredientModels = [.. RecipeState.Value.Item.IngredientModels, recipeIngredient];

            Dispatcher.Dispatch(new SetItemAction<IRecipeModel>(RecipeState.Value.Item));
        }

        _ddlVisibility = false;

        await Task.CompletedTask;
    }

    private void RemoveIngredient(IRecipeIngredientModel ingredient)
    {
        RecipeState.Value.Item.IngredientModels.Remove(ingredient);

        RecipeState.Value.Item.IngredientModels = [.. RecipeState.Value.Item.IngredientModels.Where(c => !string.Equals(c.IngredientModel.Name, ingredient.IngredientModel.Name, StringComparison.InvariantCultureIgnoreCase))];

        Dispatcher.Dispatch(new SetItemAction<IRecipeModel>(RecipeState.Value.Item));
    }

    private bool _ddlVisibility = false;

    private string DdlVisibilityCssClass => _ddlVisibility ? "visible" : "hidden";

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (RecipeState.Value.FilteredIngredients.Count() == 0)
        {
            await SearchIngredients();
        }

        _selectedIngredient = IngredientFactory.CreateIngredient();

        var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

        var authorizationResult = await AuthorizationService.AuthorizeAsync(authenticationState.User, FeatureFlagsConstants.IngredientManagementFeature);

        _canManageIngredient = authorizationResult.Succeeded;
    }

    private void CloseIngredientModal(bool isUpdated) => Dispatcher.Dispatch(new SetIngredientModalVisibilityAction(false));

    private IIngredientModel? _selectedIngredient;

    private bool _canManageIngredient;

    private async Task OpenIngredientFormModalAsync()
    {
        if (!_canManageIngredient)
            return;

        Dispatcher.Dispatch(new SetIngredientModalVisibilityAction(true));

        await Task.CompletedTask;
    }

    private bool _isMouseOverDropdown = false;
    private void OnMouseEnterDropdown() => _isMouseOverDropdown = true;

    private void OnMouseLeaveDropdown() => _isMouseOverDropdown = false;

    private async Task HideDropdownlist()
    {
        await Task.Delay(150);

        if (!_isMouseOverDropdown)
            _ddlVisibility = false;

        StateHasChanged();
    }
}