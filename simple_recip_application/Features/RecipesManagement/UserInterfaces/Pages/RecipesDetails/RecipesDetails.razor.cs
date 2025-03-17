using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Components.OptionsMenu;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.Store;
using simple_recip_application.Features.RecipesManagement.Store.Actions;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipesManagement.UserInterfaces.Pages.RecipesDetails;

public partial class RecipesDetails
{
    [Parameter] public Guid Id { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IState<RecipeState> RecipeState { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Dispatcher.Dispatch(new LoadItemAction<IRecipeModel>(Id));
    }

    private void CloseRecipeFormModal(bool isUpdated) => Dispatcher.Dispatch(new SetRecipeFormModalVisibilityAction(false));

    private List<OptionMenuItem> GetOptions()
    {
        List<OptionMenuItem> options = [
            new ("update", string.Empty, () => OpenRecipFormModalAsync(), LabelsTranslator.Update),
        ];

        return options;
    }

    private async Task OpenRecipFormModalAsync()
    {
        Dispatcher.Dispatch(new SetRecipeFormModalVisibilityAction(true));

        await Task.CompletedTask;
    }
}