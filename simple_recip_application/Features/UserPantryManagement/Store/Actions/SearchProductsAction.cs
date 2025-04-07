using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.UserPantryManagement.Store.Actions;

public record SearchProductsAction(string SearchTerm);
public record SearchProductsSuccessAction(IEnumerable<IProductModel> ProductModels);
public record SearchProductsFailureAction();
