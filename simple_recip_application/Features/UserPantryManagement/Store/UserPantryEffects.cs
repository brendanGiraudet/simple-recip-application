using System.Linq.Expressions;
using Fluxor;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.UserPantryManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.UserPantryManagement.Store.Actions;

namespace simple_recip_application.Features.UserPantryManagement.Store;

public class UserPantryEffects
(
    IUserPantryItemRepository _userPantryItemRepository,
    IIngredientRepository _ingredientRepository,
    IHouseholdProductRepository _productRepository,
    IState<UserPantryState> _userPantryState
)
{
    [EffectMethod]
    public async Task HandleLoadAction(LoadUserPantryItemsAction action, IDispatcher dispatcher)
    {
        var userPantryItemsResult = await _userPantryItemRepository.GetUserPantryItemsAsync(action.UserId);

        if (userPantryItemsResult.Success && userPantryItemsResult.Item is not null)
            dispatcher.Dispatch(new LoadUserPantryItemsSuccessAction(userPantryItemsResult.Item));
        else
            dispatcher.Dispatch(new LoadUserPantryItemsFailureAction());
    }

    [EffectMethod]
    public async Task HandleAddOrUpdateAction(AddOrUpdateUserPantryItemAction action, IDispatcher dispatcher)
    {
        var existingResult = await _userPantryItemRepository.GetUserPantryItemAsync(action.Item.UserId, action.Item.ProductId);
        if(!existingResult.Success)
            dispatcher.Dispatch(new AddOrUpdateUserPantryItemFailureAction());

        var result = existingResult.Item is null
            ? await _userPantryItemRepository.AddAsync(action.Item)
            : await _userPantryItemRepository.UpdateAsync(action.Item);

        dispatcher.Dispatch(result.Success
            ? new AddOrUpdateUserPantryItemSuccessAction(action.Item)
            : new AddOrUpdateUserPantryItemFailureAction());
    }

    [EffectMethod]
    public async Task HandleDeleteAction(DeleteUserPantryItemAction action, IDispatcher dispatcher)
    {
        var result = await _userPantryItemRepository.DeleteAsync(action.Item);

        dispatcher.Dispatch(result.Success
            ? new DeleteUserPantryItemSuccessAction(action.Item)
            : new DeleteUserPantryItemFailureAction());
    }
    
    [EffectMethod]
    public async Task HandleSearchProductsAction(SearchProductsAction action, IDispatcher dispatcher)
    {
        var take = _userPantryState.Value.Take;
        var skip = _userPantryState.Value.Skip;

        IEnumerable<Guid?> alreadyExistedProductIds = _userPantryState.Value.Items.Select(c => (Guid?)c.ProductId);

        Expression<Func<IIngredientModel, bool>>? ingredientPredicate = c => !alreadyExistedProductIds.Contains(c.Id);

        if(!string.IsNullOrWhiteSpace(action.SearchTerm))
            ingredientPredicate = c => !alreadyExistedProductIds.Contains(c.Id) && string.Equals(c.Name.ToLower(), action.SearchTerm.ToLower());

        var ingredientsResult = await _ingredientRepository.GetAsync(take, skip, ingredientPredicate);

        if(!ingredientsResult.Success)
        {
            dispatcher.Dispatch(new SearchProductsFailureAction());

            return;
        }

        Expression<Func<IHouseholdProductModel, bool>>? productPredicate = c => !alreadyExistedProductIds.Contains(c.Id);

        if (!string.IsNullOrWhiteSpace(action.SearchTerm))
            productPredicate = c => !alreadyExistedProductIds.Contains(c.Id) && string.Equals(c.Name.ToLower(), action.SearchTerm.ToLower());

        var productsResult = await _productRepository.GetAsync(take, skip, productPredicate);

        if (!productsResult.Success)
        {
            dispatcher.Dispatch(new SearchProductsFailureAction());
            return;
        }

        var products = productsResult.Item.Cast<IProductModel>()
            .Concat(ingredientsResult.Item)
            .OrderBy(c => c.Name)
            .Take(take)
            .Skip(skip)
            .ToList();

        dispatcher.Dispatch(new SearchProductsSuccessAction(products));
    }
}