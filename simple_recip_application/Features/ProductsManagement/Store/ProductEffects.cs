using Fluxor;
using simple_recip_application.Features.ProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.ProductsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.ProductsManagement.Store.Actions;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.ProductsManagement.Store;

public class ProductEffects
(
    IProductRepository _repository,
    ILogger<ProductEffects> _logger
)
{
    [EffectMethod]
    public async Task HandleLoadProducts(LoadItemsAction<IProductModel> action, IDispatcher dispatcher)
    {
        try
        {
            var ingredientsResult = await _repository.GetAsync(action.Take, action.Skip, action.Predicate);

            if (ingredientsResult.Success)
                dispatcher.Dispatch(new LoadItemsSuccessAction<IProductModel>(ingredientsResult.Item));

            else
                dispatcher.Dispatch(new LoadItemsFailureAction<IProductModel>());

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors du chargement des ingrédients");

            dispatcher.Dispatch(new LoadItemsFailureAction<IProductModel>());
        }
    }

    [EffectMethod]
    public async Task HandleAddProduct(AddItemAction<IProductModel> action, IDispatcher dispatcher)
    {
        try
        {
            var addResult = await _repository.AddAsync(action.Item);

            if (!addResult.Success)
                dispatcher.Dispatch(new AddItemFailureAction<IProductModel>(action.Item));

            else
            {
                dispatcher.Dispatch(new AddItemSuccessAction<IProductModel>(action.Item));
                dispatcher.Dispatch(new SetProductModalVisibilityAction(false));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de l'ajout d'un ingrédient");

            dispatcher.Dispatch(new AddItemFailureAction<IProductModel>(action.Item));
        }
    }

    [EffectMethod]
    public async Task HandleDeleteProduct(DeleteItemAction<IProductModel> action, IDispatcher dispatcher)
    {
        try
        {
            if (!action.Item.Id.HasValue)
            {
                dispatcher.Dispatch(new DeleteItemFailureAction<IProductModel>(action.Item));

                return;
            }

            var ingredientResult = await _repository.GetByIdAsync(action.Item.Id.Value);
            if (!ingredientResult.Success || ingredientResult.Item == null)
            {
                dispatcher.Dispatch(new DeleteItemFailureAction<IProductModel>(action.Item));

                return;
            }

            var deleteResult = await _repository.DeleteAsync(ingredientResult.Item);

            if (!deleteResult.Success)
                dispatcher.Dispatch(new DeleteItemFailureAction<IProductModel>(action.Item));

            else
            {
                dispatcher.Dispatch(new DeleteItemSuccessAction<IProductModel>(action.Item));
                dispatcher.Dispatch(new SetProductModalVisibilityAction(false));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la suppression de l'ingrédient");

            dispatcher.Dispatch(new DeleteItemFailureAction<IProductModel>(action.Item));
        }
    }

    [EffectMethod]
    public async Task HandleUpdateProduct(UpdateItemAction<IProductModel> action, IDispatcher dispatcher)
    {
        try
        {
            var updateResult = await _repository.UpdateAsync(action.Item);

            if (!updateResult.Success)
                dispatcher.Dispatch(new UpdateItemFailureAction<IProductModel>(action.Item));

            else
            {
                dispatcher.Dispatch(new UpdateItemSuccessAction<IProductModel>(action.Item));
                dispatcher.Dispatch(new SetProductModalVisibilityAction(false));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la mise à jour");

            dispatcher.Dispatch(new UpdateItemFailureAction<IProductModel>(action.Item));
        }
    }
}
