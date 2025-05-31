using Fluxor;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Repositories;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.HouseholdProductsManagement.Store;

public class HouseholdProductEffects
(
    IServiceScopeFactory _scopeFactory,
    ILogger<HouseholdProductEffects> _logger
)
{
    [EffectMethod]
    public async Task HandleLoadHouseholdProducts(LoadItemsAction<IHouseholdProductModel> action, IDispatcher dispatcher)
    {
        try
        {
            await Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IHouseholdProductRepository>();

                var ingredientsResult = await repository.GetAsync(action.Take, action.Skip, action.Predicate);

                if (ingredientsResult.Success)
                    dispatcher.Dispatch(new LoadItemsSuccessAction<IHouseholdProductModel>(ingredientsResult.Item));

                else
                    dispatcher.Dispatch(new LoadItemsFailureAction<IHouseholdProductModel>());
            });

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors du chargement des ingrédients");

            dispatcher.Dispatch(new LoadItemsFailureAction<IHouseholdProductModel>());
        }
    }

    [EffectMethod]
    public async Task HandleAddHouseholdProduct(AddItemAction<IHouseholdProductModel> action, IDispatcher dispatcher)
    {
        try
        {
            await Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IHouseholdProductRepository>();

                var addResult = await repository.AddAsync(action.Item);

                if (!addResult.Success)
                    dispatcher.Dispatch(new AddItemFailureAction<IHouseholdProductModel>(action.Item));

                else
                {
                    dispatcher.Dispatch(new AddItemSuccessAction<IHouseholdProductModel>(action.Item));
                    dispatcher.Dispatch(new SetFormModalVisibilityAction<IHouseholdProductModel>(false));
                }

            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de l'ajout d'un ingrédient");

            dispatcher.Dispatch(new AddItemFailureAction<IHouseholdProductModel>(action.Item));
        }
    }

    [EffectMethod]
    public async Task HandleDeleteHouseholdProduct(DeleteItemAction<IHouseholdProductModel> action, IDispatcher dispatcher)
    {
        try
        {
            await Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IHouseholdProductRepository>();

                if (!action.Item.Id.HasValue)
                {
                    dispatcher.Dispatch(new DeleteItemFailureAction<IHouseholdProductModel>(action.Item));

                    return;
                }

                var ingredientResult = await repository.GetByIdAsync(action.Item.Id.Value);
                if (!ingredientResult.Success || ingredientResult.Item == null)
                {
                    dispatcher.Dispatch(new DeleteItemFailureAction<IHouseholdProductModel>(action.Item));

                    return;
                }

                var deleteResult = await repository.DeleteAsync(ingredientResult.Item);

                if (!deleteResult.Success)
                    dispatcher.Dispatch(new DeleteItemFailureAction<IHouseholdProductModel>(action.Item));

                else
                {
                    dispatcher.Dispatch(new DeleteItemSuccessAction<IHouseholdProductModel>(action.Item));
                    dispatcher.Dispatch(new SetFormModalVisibilityAction<IHouseholdProductModel>(false));
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la suppression de l'ingrédient");

            dispatcher.Dispatch(new DeleteItemFailureAction<IHouseholdProductModel>(action.Item));
        }
    }

    [EffectMethod]
    public async Task HandleUpdateHouseholdProduct(UpdateItemAction<IHouseholdProductModel> action, IDispatcher dispatcher)
    {
        try
        {
            await Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IHouseholdProductRepository>();

                var updateResult = await repository.UpdateAsync(action.Item);

                if (!updateResult.Success)
                    dispatcher.Dispatch(new UpdateItemFailureAction<IHouseholdProductModel>(action.Item));

                else
                {
                    dispatcher.Dispatch(new UpdateItemSuccessAction<IHouseholdProductModel>(action.Item));
                    dispatcher.Dispatch(new SetFormModalVisibilityAction<IHouseholdProductModel>(false));
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la mise à jour");

            dispatcher.Dispatch(new UpdateItemFailureAction<IHouseholdProductModel>(action.Item));
        }
    }
}
