﻿using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.ShoppingListManagement.Store.Actions;

public record AddOrUpdateShoppingListItemAction(IShoppingListItemModel Item);
public record AddOrUpdateShoppingListItemSuccessAction(IShoppingListItemModel Item);
public record AddOrUpdateShoppingListItemFailureAction();