using Fluxor;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Store;

namespace simple_recip_application.Features.IngredientsManagement.Store;

[FeatureState]
public record class IngredientState : EntityBaseState<IIngredientModel>
{
    
}