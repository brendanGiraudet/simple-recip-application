using Fluxor;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore;
using simple_recip_application.Store;

namespace simple_recip_application.Features.IngredientsManagement.Store;

[FeatureState]
public record class IngredientState : BaseState
{
    public IEnumerable<IIngredientModel> Ingredients { get; set; } = [];
    public IEnumerable<IIngredientModel> SelectedIngredients { get; set; } = [];
}