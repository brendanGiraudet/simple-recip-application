using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using simple_recip_application.Features.IngredientsManagement.Store.Actions;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;
using simple_recip_application.Resources;
using Fluxor;
using simple_recip_application.Features.IngredientsManagement.Store;
using Fluxor.Blazor.Web.Components;

namespace simple_recip_application.Features.IngredientsManagement.UserInterfaces.Components;
public class IngredientFormBase : FluxorComponent
{
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IStringLocalizer<Labels> LabelsLocalizer { get; set; }
    [Inject] public required IStringLocalizer<Messages> MessagesLocalizer { get; set; }
    [Inject] public required IState<IngredientState> IngredientState { get; set; }
    
    [Parameter] public Guid? IngredientId { get; set; }
    [Parameter] public EventCallback<bool> OnCancel { get; set; }
    
    protected IngredientModel Ingredient { get; set; } = new();
    protected bool IsSuccess { get; set; } = false;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (IngredientId.HasValue)
        {
            // Charger l’ingrédient existant
            Ingredient = IngredientState.Value.Ingredients.Find(i => i.Id == IngredientId) ?? new IngredientModel();
        }
    }

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
        if (IngredientId.HasValue)
        {
            Dispatcher.Dispatch(new UpdateIngredientAction(Ingredient));
        }
        else
        {
            Dispatcher.Dispatch(new AddIngredientAction(Ingredient));
        }

        IsSuccess = true;
    }

    protected string GetSuccessVisibilityCssClass() => IsSuccess ? "" : "hidden";
}