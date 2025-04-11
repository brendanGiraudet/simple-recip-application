using System.Linq.Expressions;
using Fluxor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using simple_recip_application.Constants;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Factories;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.TagsManagement.UserInterfaces.Pages;

public partial class Tags
{
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IState<TagState> TagState { get; set; }
    [Inject] public required ITagFactory ITagFactory { get; set; }
    [Inject] public required IAuthorizationService AuthorizationService { get; set; }
    [Inject] public required AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    private string newTagName = string.Empty;
    private bool _canManageTag;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

        var authorizationResult = await AuthorizationService.AuthorizeAsync(authenticationState.User, FeatureFlagsConstants.TagManagement);

        _canManageTag = authorizationResult.Succeeded;
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        LoadFilteredTags();
    }

    private string GetTagsVisibilityCssClass() => !TagState.Value.IsLoading ? "" : "hidden";

    private bool CanPreviousClick() => TagState.Value.Skip > 0;
    private async Task OnPrevious()
    {
        if (!CanPreviousClick()) return;

        var skip = TagState.Value.Skip - TagState.Value.Take;

        skip = skip < 0 ? 0 : skip;

        LoadFilteredTags(skip);

        await Task.CompletedTask;
    }

    private async Task OnNext()
    {
        var skip = TagState.Value.Skip + TagState.Value.Take;

        LoadFilteredTags(skip);

        await Task.CompletedTask;
    }

    private string _searchTerm = string.Empty;

    private void OnSearchTermChanged(string searchTerm)
    {
        _searchTerm = searchTerm;
        LoadFilteredTags();
    }

    private void LoadFilteredTags(int? skip = null)
    {
        Expression<Func<ITagModel, bool>>? filter = c => c.RemoveDate == null;

        if (!string.IsNullOrEmpty(_searchTerm))
            filter = i => i.Name.ToLower().Contains(_searchTerm.ToLower()) && i.RemoveDate == null;

        Dispatcher.Dispatch(new LoadItemsAction<ITagModel>(Take: TagState.Value.Take, Skip: skip ?? 0, filter));
    }

    private void DeleteTag(ITagModel tagModel) => Dispatcher.Dispatch(new DeleteItemAction<ITagModel>(tagModel));
    private void AddTag()
    {
        var tagModel = ITagFactory.Create(_searchTerm);
        Dispatcher.Dispatch(new AddItemAction<ITagModel>(tagModel));
    }
}
