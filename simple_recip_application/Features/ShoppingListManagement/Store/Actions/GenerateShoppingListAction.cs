﻿using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entities;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.ShoppingListManagement.Store.Actions;

public record GenerateShoppingListAction(IEnumerable<IRecipeModel> Recipes, string UserId);
public record GenerateShoppingListSuccessAction(IEnumerable<IShoppingListItemModel> Items);
public record GenerateShoppingListFailureAction();