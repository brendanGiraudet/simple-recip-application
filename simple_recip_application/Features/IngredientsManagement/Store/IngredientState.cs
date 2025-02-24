using Fluxor;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore;

namespace simple_recip_application.Features.IngredientsManagement.Store;

[FeatureState]
public record class IngredientState
{
    public List<IIngredientModel> Ingredients { get; set; } = [];
    public List<IIngredientModel> SelectedIngredients { get; set; } = [];
    public bool IsLoading { get; set; } = false;
}