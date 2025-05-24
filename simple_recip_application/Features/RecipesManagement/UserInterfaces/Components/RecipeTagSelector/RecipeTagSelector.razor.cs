using System.Linq.Expressions;
using Fluxor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using simple_recip_application.Constants;
using simple_recip_application.Features.NotificationsManagement.Store;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Factories;
using simple_recip_application.Features.RecipesManagement.Store;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Factories;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipesManagement.UserInterfaces.Components.RecipeTagSelector;

public partial class RecipeTagSelector
{
    [Inject] public required IState<RecipeState> RecipeState { get; set; }
    [Inject] public required IState<TagState> TagState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IRecipeTagFactory RecipeTagFactory { get; set; }
    [Inject] public required ITagFactory TagFactory { get; set; }
    [Inject] public required IAuthorizationService AuthorizationService { get; set; }
    [Inject] public required AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    private ICollection<IRecipeTagModel> RecipeTags => RecipeState.Value.Item?.TagModels ?? [];

    private async Task SearchTags(string? searchTerm = null)
    {
        Expression<Func<ITagModel, bool>>? filter = c => c.RemoveDate == null;

        if (!string.IsNullOrEmpty(searchTerm))
            filter = i => i.Name.ToLower().Contains(searchTerm.ToLower()) && i.RemoveDate == null;

        Dispatcher.Dispatch(new LoadItemsAction<ITagModel>(Take: TagState.Value.Take, Skip: 0, filter));

        await Task.CompletedTask;
    }

    private async Task AddTag(ITagModel tag)
    {
        if (!RecipeState.Value.Item.TagModels.Any(i => i.TagModel == tag))
        {
            var recipeTag = RecipeTagFactory.Create(RecipeState.Value.Item, tag);

            RecipeState.Value.Item.TagModels = [.. RecipeState.Value.Item.TagModels, recipeTag];

            Dispatcher.Dispatch(new SetItemAction<IRecipeModel>(RecipeState.Value.Item));
        }

        await Task.CompletedTask;
    }

    private void RemoveTag(IRecipeTagModel ingredient)
    {
        RecipeState.Value.Item.TagModels.Remove(ingredient);

        RecipeState.Value.Item.TagModels = [.. RecipeState.Value.Item.TagModels.Where(c => !string.Equals(c.TagModel.Name, ingredient.TagModel.Name, StringComparison.InvariantCultureIgnoreCase))];

        Dispatcher.Dispatch(new SetItemAction<IRecipeModel>(RecipeState.Value.Item));
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (RecipeState.Value.FilteredTags.Count() == 0)
        {
            await SearchTags();
        }

        var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

        var authorizationResult = await AuthorizationService.AuthorizeAsync(authenticationState.User, FeatureFlagsConstants.TagManagement);

        _canManageTag = authorizationResult.Succeeded;
    }

    private bool _canManageTag;

    private async Task CreateTag()
    {
        if (!_canManageTag) return;

        //TODO faire la creation de tag 
        //if (string.IsNullOrWhiteSpace(_searchTerm)) return;

        //var tagModel = ITagFactory.Create(_searchTerm);
        //Dispatcher.Dispatch(new AddItemAction<ITagModel>(tagModel));
    }
}