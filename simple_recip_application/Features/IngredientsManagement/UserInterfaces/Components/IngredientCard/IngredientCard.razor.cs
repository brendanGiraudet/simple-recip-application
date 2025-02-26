using Microsoft.AspNetCore.Components;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore;

namespace simple_recip_application.Features.IngredientsManagement.UserInterfaces.Components.IngredientCard;

public partial class IngredientCard
{
    [Parameter] public required IIngredientModel Ingredient { get; set; }
    [Parameter] public EventCallback<IIngredientModel> OnEdit { get; set; }
    [Parameter] public EventCallback<IIngredientModel> OnDelete { get; set; }
    [Parameter] public EventCallback<IIngredientModel> OnSelect { get; set; }
    [Parameter] public bool IsSelected { get; set; }

    private void ToggleSelection()
    {
        if(OnSelect.HasDelegate)
            OnSelect.InvokeAsync(Ingredient);
    }

    private string GetSelectionIconClass() => IsSelected ? "selected" : "not-selected";

    protected string GetImageSource()
    {
        if (Ingredient.Image != null && Ingredient.Image.Length > 0)
        {
            var base64String = Convert.ToBase64String(Ingredient.Image);
            return $"data:image/png;base64,{base64String}";
        }
        return "placeholder.png"; // Image par défaut
    }

    protected async Task OnEditAsync(IIngredientModel model)
    {
        if (OnEdit.HasDelegate)
            await OnEdit.InvokeAsync(model);
    }
}