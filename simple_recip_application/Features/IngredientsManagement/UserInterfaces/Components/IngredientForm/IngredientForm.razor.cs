using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using simple_recip_application.Features.IngredientsManagement.Store.Actions;
using simple_recip_application.Resources;
using Fluxor;
using simple_recip_application.Features.IngredientsManagement.Store;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore;

namespace simple_recip_application.Features.IngredientsManagement.UserInterfaces.Components.IngredientForm;

public partial class IngredientForm
{
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IStringLocalizer<Labels> LabelsLocalizer { get; set; }
    [Inject] public required IStringLocalizer<Messages> MessagesLocalizer { get; set; }
    [Inject] public required IState<IngredientState> IngredientState { get; set; }
    
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
            Dispatcher.Dispatch(new UpdateIngredientAction(Ingredient));

        else
            Dispatcher.Dispatch(new AddIngredientAction(Ingredient));
    }
}