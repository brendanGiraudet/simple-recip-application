﻿using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.ShoppingListManagement.Store.Actions;

public record LoadShoppingListItemsAction(string UserId);
public record LoadShoppingListItemsSuccessAction(IEnumerable<IShoppingListItemModel> Items);
public record LoadShoppingListItemsFailureAction();