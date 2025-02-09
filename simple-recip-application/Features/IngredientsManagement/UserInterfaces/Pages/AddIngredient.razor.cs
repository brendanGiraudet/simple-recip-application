using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;
using simple_recip_application.Features.IngredientsManagement.Store.Actions;

namespace simple_recip_application.Features.IngredientsManagement.Pages;

public class AddIngredientBase : Fluxor.Blazor.Web.Components.FluxorComponent
{
    [Inject] protected IDispatcher Dispatcher { get; set; } = default!;

    protected IngredientModel Ingredient { get; set; } = new();
    protected string? ErrorMessage { get; set; }
    protected bool IsSuccess { get; set; } = false;

    protected void AddIngredient()
    {
        if (string.IsNullOrWhiteSpace(Ingredient.Name))
        {
            ErrorMessage = "Le nom de l'ingrédient est requis.";
            IsSuccess = false;
            return;
        }

        Dispatcher.Dispatch(new AddIngredientAction(Ingredient));

        // Réinitialisation
        IsSuccess = true;
        ErrorMessage = null;
        Ingredient = new();
    }

    protected string GetErrorVisibilityCssClass() => !string.IsNullOrEmpty(ErrorMessage) ? "" : "hidden";
    protected string GetSuccessVisibilityCssClass() => IsSuccess ? "" : "hidden";
}
