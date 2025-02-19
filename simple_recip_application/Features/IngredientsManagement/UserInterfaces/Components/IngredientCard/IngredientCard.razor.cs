using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;
using simple_recip_application.Resources;

namespace simple_recip_application.Features.IngredientsManagement.UserInterfaces.Components.IngredientCard;

public partial class IngredientCard
{
    [Parameter] public required IngredientModel Ingredient { get; set; }
    [Parameter] public EventCallback<Guid> OnEdit { get; set; }
    [Parameter] public EventCallback<Guid> OnDelete { get; set; }
    [Inject] protected IStringLocalizer<Labels> LabelsLocalizer { get; set; } = default!;

    protected string GetImageSource()
    {
        if (Ingredient.Image != null && Ingredient.Image.Length > 0)
        {
            var base64String = Convert.ToBase64String(Ingredient.Image);
            return $"data:image/png;base64,{base64String}";
        }
        return "placeholder.png"; // Image par défaut
    }

    protected async Task OnEditAsync(Guid guid)
    {
        if (OnEdit.HasDelegate)
            await OnEdit.InvokeAsync(guid);
    }
    
    protected async Task OnDeleteAsync(Guid guid)
    {
        if (OnDelete.HasDelegate)
            await OnDelete.InvokeAsync(guid);
    }
}