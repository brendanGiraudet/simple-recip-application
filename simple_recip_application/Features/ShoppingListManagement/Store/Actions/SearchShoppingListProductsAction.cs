﻿using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.ShoppingList.Store.Actions;

public record SearchShoppingListProductsAction(string SearchTerm);
public record SearchShoppingListProductsSuccessAction(IEnumerable<IProductModel> ProductModels);
public record SearchShoppingListProductsFailureAction();
