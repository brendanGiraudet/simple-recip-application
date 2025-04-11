using Fluxor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using simple_recip_application.Components.OptionsMenu;
using simple_recip_application.Constants;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.Store;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipesManagement.UserInterfaces.Pages.RecipesDetails;

public partial class RecipesDetails
{
    [Parameter] public Guid Id { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IState<RecipeState> RecipeState { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }
    [Inject] public required IAuthorizationService AuthorizationService { get; set; }
    [Inject] public required AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    private void CloseRecipeFormModal(bool isUpdated) => Dispatcher.Dispatch(new SetFormModalVisibilityAction<IRecipeModel>(false));

    private List<OptionMenuItem> GetOptions()
    {
        List<OptionMenuItem> options = [
            new (MaterialIconsConstants.Update, string.Empty, () => OpenRecipFormModalAsync(), LabelsTranslator.Update),
        ];

        return options;
    }

    private async Task OpenRecipFormModalAsync()
    {
        Dispatcher.Dispatch(new SetFormModalVisibilityAction<IRecipeModel>(true));

        await Task.CompletedTask;
    }

    private bool _canManageRecipe = false;
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

        var authorizationResult = await AuthorizationService.AuthorizeAsync(authenticationState.User, FeatureFlagsConstants.RecipeManagementFeature);

        _canManageRecipe = authorizationResult.Succeeded;

        if (_canManageRecipe)
            Dispatcher.Dispatch(new LoadItemAction<IRecipeModel>(Id));
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!_canManageRecipe)
        {
            NavigationManager.NavigateTo(PageUrlsConstants.RecipesPage);
            return;
        }
    }
}