using System.Linq.Expressions;
using Fluxor;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.ShoppingList.Store.Actions;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.ShoppingListManagement.Store.Actions;

namespace simple_recip_application.Features.ShoppingListManagement.Store;

public class ShoppingListEffects
(
    IServiceScopeFactory _scopeFactory,
    IState<ShoppingListState> _userPantryState
)
{
    [EffectMethod]
    public async Task HandleLoadAction(LoadShoppingListItemsAction action, IDispatcher dispatcher)
    {
        await Task.Run(async () =>
        {
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IShoppingListRepository>();

            var userPantryItemsResult = await repository.GetShoppingListItemsAsync(action.UserId);

            if (userPantryItemsResult.Success && userPantryItemsResult.Item is not null)
                dispatcher.Dispatch(new LoadShoppingListItemsSuccessAction(userPantryItemsResult.Item));
            else
                dispatcher.Dispatch(new LoadShoppingListItemsFailureAction());
        });
    }

    [EffectMethod]
    public async Task HandleAddOrUpdateAction(AddOrUpdateShoppingListItemAction action, IDispatcher dispatcher)
    {
        await Task.Run(async () =>
        {
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IShoppingListRepository>();

            var existingResult = await repository.GetShoppingListItemAsync(action.Item.UserId, action.Item.ProductId);
            if (!existingResult.Success)
                dispatcher.Dispatch(new AddOrUpdateShoppingListItemFailureAction());

            var result = existingResult.Item is null
                ? await repository.AddAsync(action.Item)
                : await repository.UpdateAsync(action.Item);

            dispatcher.Dispatch(result.Success
                ? new AddOrUpdateShoppingListItemSuccessAction(action.Item)
                : new AddOrUpdateShoppingListItemFailureAction());
        });
    }

    [EffectMethod]
    public async Task HandleDeleteAction(DeleteShoppingListItemAction action, IDispatcher dispatcher)
    {
        await Task.Run(async () =>
        {
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IShoppingListRepository>();

            var result = await repository.DeleteAsync(action.Item);

            dispatcher.Dispatch(result.Success
                ? new DeleteShoppingListItemSuccessAction(action.Item)
                : new DeleteShoppingListItemFailureAction());
        });
    }

    [EffectMethod]
    public async Task HandleSearchProductsAction(SearchShoppingListProductsAction action, IDispatcher dispatcher)
    {
        await Task.Run(async () =>
        {
            using var scope = _scopeFactory.CreateScope();
            var ingredientRepository = scope.ServiceProvider.GetRequiredService<IIngredientRepository>();
            var productRepository = scope.ServiceProvider.GetRequiredService<IHouseholdProductRepository>();

            var take = _userPantryState.Value.Take;
            var skip = _userPantryState.Value.Skip;

            IEnumerable<Guid?> alreadyExistedProductIds = _userPantryState.Value.Items.Select(c => (Guid?)c.ProductId);

            Expression<Func<IIngredientModel, bool>>? ingredientPredicate = c => !alreadyExistedProductIds.Contains(c.Id);

            if (!string.IsNullOrWhiteSpace(action.SearchTerm))
                ingredientPredicate = c => !alreadyExistedProductIds.Contains(c.Id) && c.Name.ToLower().Contains(action.SearchTerm.ToLower());

            var ingredientsResult = await ingredientRepository.GetAsync(take, skip, ingredientPredicate);

            if (!ingredientsResult.Success)
            {
                dispatcher.Dispatch(new SearchShoppingListProductsFailureAction());

                return;
            }

            Expression<Func<IHouseholdProductModel, bool>>? productPredicate = c => !alreadyExistedProductIds.Contains(c.Id);

            if (!string.IsNullOrWhiteSpace(action.SearchTerm))
                productPredicate = c => !alreadyExistedProductIds.Contains(c.Id) && c.Name.ToLower().Contains(action.SearchTerm.ToLower());

            var productsResult = await productRepository.GetAsync(take, skip, productPredicate);

            if (!productsResult.Success)
            {
                dispatcher.Dispatch(new SearchShoppingListProductsFailureAction());
                return;
            }

            var products = productsResult.Item.Cast<IProductModel>()
                .Concat(ingredientsResult.Item)
                .OrderBy(c => c.Name)
                .Take(take)
                .Skip(skip)
                .ToList();

            dispatcher.Dispatch(new SearchShoppingListProductsSuccessAction(products));
        });
    }
}