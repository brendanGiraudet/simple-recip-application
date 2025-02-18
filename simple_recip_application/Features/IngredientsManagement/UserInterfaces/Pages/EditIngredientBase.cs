using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;
using simple_recip_application.Features.IngredientsManagement.Store;
using simple_recip_application.Features.IngredientsManagement.Store.Actions;
using simple_recip_application.Resources;

namespace simple_recip_application.Features.IngredientsManagement.UserInterfaces.Pages;

public class EditIngredientBase : Fluxor.Blazor.Web.Components.FluxorComponent
{
    [Inject] protected IDispatcher Dispatcher { get; set; } = default!;
    [Inject] protected NavigationManager Navigation { get; set; } = default!;
    [Inject] protected IStringLocalizer<Labels> LabelsLocalizer { get; set; } = default!;
    [Inject] protected IStringLocalizer<Messages> MessagesLocalizer { get; set; } = default!;
    [Inject] protected IState<IngredientState> IngredientState { get; set; } = default!;
    [Parameter] public string Id { get; set; }

    protected IngredientModel Ingredient { get; set; } = new();
    protected bool IsSuccess { get; set; } = false;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        // Charger l'ingrédient depuis le store en fonction de l'ID
        Ingredient = IngredientState.Value.Ingredients.Find(i => string.Equals(i.Id.ToString(), Id, StringComparison.InvariantCultureIgnoreCase));
        
        if (Ingredient == null)
        {
            Navigation.NavigateTo("/ingredients"); // Redirection si l'ingrédient n'existe pas
        }
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

    protected void UpdateIngredient()
    {
        Dispatcher.Dispatch(new UpdateIngredientAction(Ingredient));
        IsSuccess = true;
    }

    protected string GetSuccessVisibilityCssClass() => IsSuccess ? "" : "hidden";
}
