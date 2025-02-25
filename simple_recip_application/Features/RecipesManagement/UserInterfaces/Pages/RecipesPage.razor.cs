using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using simple_recip_application.Components.OptionsMenu;
using simple_recip_application.Features.RecipesManagement.ApplicationCore;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;
using simple_recip_application.Features.RecipesManagement.Store;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipesManagement.UserInterfaces.Pages;

public partial class RecipesPage
{
    [Inject] public required IState<RecipeState> RecipeState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public IStringLocalizer<Labels> LabelsLocalizer { get; set; } = default!;

   private bool _isRecipeFormModalOpen { get; set; } = false;
    private IRecipeModel? _selectedRecipe { get; set; } = new RecipeModel();

    private async Task OpenRecipFormModalAsync(IRecipeModel? model = null)
    {
        _selectedRecipe = model ?? new RecipeModel();
        _isRecipeFormModalOpen = true;

        StateHasChanged();
    }

    private void CloseRecipeFormModal(bool isUpdated) => _isRecipeFormModalOpen = false;

    private void DeleteRecipe(IRecipeModel recipe)
    {
        Dispatcher.Dispatch(new DeleteItemAction<IRecipeModel>(recipe));
    }

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
            new ("add", LabelsLocalizer["AddRecipe"], () => OpenRecipFormModalAsync())
        ];

        return options;
    }
}