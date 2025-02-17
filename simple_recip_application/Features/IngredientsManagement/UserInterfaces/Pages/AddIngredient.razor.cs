using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;
using simple_recip_application.Features.IngredientsManagement.Store.Actions;
using simple_recip_application.Resources;

namespace simple_recip_application.Features.IngredientsManagement.UserInterfaces.Pages;

public class AddIngredientBase : Fluxor.Blazor.Web.Components.FluxorComponent
{
    [Inject] protected IDispatcher Dispatcher { get; set; } = default!;
    [Inject] protected IStringLocalizer<Labels> LabelsLocalizer { get; set; } = default!;
    [Inject] protected IStringLocalizer<Messages> MessagesLocalizer { get; set; } = default!;

    protected IngredientModel Ingredient { get; set; } = new();
    protected bool IsSuccess { get; set; } = false;

    protected void AddIngredient()
    {
        Dispatcher.Dispatch(new AddIngredientAction(Ingredient));

        // Réinitialisation
        IsSuccess = true;
        Ingredient = new();
    }

    protected string GetSuccessVisibilityCssClass() => IsSuccess ? "" : "hidden";

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
}
