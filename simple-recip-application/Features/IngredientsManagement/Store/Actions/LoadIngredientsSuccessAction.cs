using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;

namespace simple_recip_application.Features.IngredientsManagement.Store.Actions;

public record LoadIngredientsSuccessAction(List<IngredientModel> Ingredients);
