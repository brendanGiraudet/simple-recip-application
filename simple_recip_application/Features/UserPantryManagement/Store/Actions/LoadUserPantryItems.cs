using simple_recip_application.Features.UserPantryManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.UserPantryManagement.Store.Actions;

public record LoadUserPantryItemsAction(string UserId);
public record LoadUserPantryItemsSuccessAction(IEnumerable<IUserPantryItemModel> Items);
public record LoadUserPantryItemsFailureAction();