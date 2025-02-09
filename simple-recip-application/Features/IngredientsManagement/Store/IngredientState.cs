using System.Collections.Generic;
using Fluxor;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;

namespace simple_recip_application.Features.IngredientsManagement.Store;

[FeatureState]
public class IngredientState
{
    public List<IngredientModel> Ingredients { get; set; } = new();
    public bool IsLoading { get; set; } = false;
    public string? ErrorMessage { get; set; }
}