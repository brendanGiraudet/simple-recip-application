using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Store.Actions;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using Fluxor;
using simple_recip_application.Features.RecipesManagement.Store;

namespace simple_recip_application.Features.RecipesManagement.UserInterfaces.Components.RecipeForm;

public partial class RecipeForm
{
    [Inject] public required IState<RecipeState> RecipeState { get; set; }

    public IRecipeModel Recipe
    {
        get
        {
            return RecipeState.Value.Item;
        }
        set
        {
            Dispatcher.Dispatch(new SetItemAction<IRecipeModel>(value));
        }
    }
    [Parameter] public EventCallback<bool> OnCancel { get; set; }

    protected async Task OnCancelAsync()
    {
        if (OnCancel.HasDelegate)
            await OnCancel.InvokeAsync(false);
    }

    protected async Task HandleImageUpload(InputFileChangeEventArgs e)
    {
        var file = e.File;
        if (file != null)
        {
            using var memoryStream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(memoryStream);
            RecipeState.Value.Item.Image = memoryStream.ToArray();
        }
    }

    protected void Submit()
    {
        if (RecipeState.Value.Item.Id.HasValue)
            Dispatcher.Dispatch(new UpdateItemAction<IRecipeModel>(Recipe));
        else
            Dispatcher.Dispatch(new AddItemAction<IRecipeModel>(Recipe));
    }

    private void DeleteRecipe(IRecipeModel model)
    {
        if (model.Id.HasValue)
            Dispatcher.Dispatch(new DeleteItemAction<IRecipeModel>(model));
    }

    private string GetDeleteButtonCssClass() => RecipeState.Value.Item.Id.HasValue ? "" : "hidden";
}