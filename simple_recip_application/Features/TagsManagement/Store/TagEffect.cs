using Fluxor;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Repositories;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.TagsManagement.Store;

public class TagEffects
(
    IServiceScopeFactory _scopeFactory,
    ILogger<TagEffects> _logger
)
{
    [EffectMethod]
    public async Task HandleLoadTags(LoadItemsAction<ITagModel> action, IDispatcher dispatcher)
    {
        try
        {
            await Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<ITagRepository>();

                var tagsResult = await repository.GetAsync(action.Take, action.Skip, action.Predicate);

                if (tagsResult.Success)
                    dispatcher.Dispatch(new LoadItemsSuccessAction<ITagModel>(tagsResult.Item));

                else
                    dispatcher.Dispatch(new LoadItemsFailureAction<ITagModel>());
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors du chargement des ingrédients");

            dispatcher.Dispatch(new LoadItemsFailureAction<ITagModel>());
        }
    }

    [EffectMethod]
    public async Task HandleAddTag(AddItemAction<ITagModel> action, IDispatcher dispatcher)
    {
        try
        {
            await Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<ITagRepository>();

                var addResult = await repository.AddAsync(action.Item);

                if (!addResult.Success)
                    dispatcher.Dispatch(new AddItemFailureAction<ITagModel>(action.Item));

                else
                {
                    dispatcher.Dispatch(new AddItemSuccessAction<ITagModel>(action.Item));
                    dispatcher.Dispatch(new SetFormModalVisibilityAction<ITagModel>(false));
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de l'ajout d'un tag");

            dispatcher.Dispatch(new AddItemFailureAction<ITagModel>(action.Item));
        }
    }

    [EffectMethod]
    public async Task HandleDeleteTag(DeleteItemAction<ITagModel> action, IDispatcher dispatcher)
    {
        try
        {
            await Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<ITagRepository>();

                if (!action.Item.Id.HasValue)
                {
                    dispatcher.Dispatch(new DeleteItemFailureAction<ITagModel>(action.Item));

                    return;
                }

                var tagResult = await repository.GetByIdAsync(action.Item.Id.Value);
                if (!tagResult.Success || tagResult.Item == null)
                {
                    dispatcher.Dispatch(new DeleteItemFailureAction<ITagModel>(action.Item));

                    return;
                }

                var deleteResult = await repository.DeleteAsync(tagResult.Item);

                if (!deleteResult.Success)
                    dispatcher.Dispatch(new DeleteItemFailureAction<ITagModel>(action.Item));

                else
                    dispatcher.Dispatch(new DeleteItemSuccessAction<ITagModel>(action.Item));
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la suppression de l'ingrédient");

            dispatcher.Dispatch(new DeleteItemFailureAction<ITagModel>(action.Item));
        }
    }
}
