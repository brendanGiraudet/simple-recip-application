using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using simple_recip_application.Resources;
using Fluxor;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.IngredientsManagement.UserInterfaces.Components.IngredientForm;

public partial class IngredientForm
{
    [Parameter] public required IIngredientModel Ingredient { get; set; }
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
            Ingredient.Image = memoryStream.ToArray();
        }
    }

    protected void Submit()
    {
        if (Ingredient.Id.HasValue)
            Dispatcher.Dispatch(new UpdateItemAction<IIngredientModel>(Ingredient));

        else
            Dispatcher.Dispatch(new AddItemAction<IIngredientModel>(Ingredient));
    }
}