using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Features.RecipesManagement.ApplicationCore;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipesManagement.UserInterfaces.Components.RecipeForm;

public partial class RecipeForm
{
    [Parameter] public required IRecipeModel Recipe { get; set; }
    [Parameter] public EventCallback<bool> OnCancel { get; set; }

    protected async Task OnCancelAsync()
    {
        if(OnCancel.HasDelegate)
            await OnCancel.InvokeAsync(false);
    }

    protected async Task HandleImageUpload(InputFileChangeEventArgs e)
    {
        var file = e.File;
        if (file != null)
        {
            using var memoryStream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(memoryStream);
            Recipe.Image = memoryStream.ToArray();
        }
    }

    protected void Submit()
    {
        if (Recipe.Id.HasValue)
            Dispatcher.Dispatch(new UpdateItemAction<IRecipeModel>(Recipe));
        else
            Dispatcher.Dispatch(new AddItemAction<IRecipeModel>(Recipe));
    }

    private void DeleteRecipe(IRecipeModel model)
    {
        if (model.Id.HasValue)
            Dispatcher.Dispatch(new DeleteItemAction<IRecipeModel>(model));
    }

    private string GetDeleteButtonCssClass() => Recipe.Id.HasValue ? "" : "hidden";
}