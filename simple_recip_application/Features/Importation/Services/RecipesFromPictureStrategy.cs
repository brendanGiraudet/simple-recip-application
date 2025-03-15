using Fluxor;
using simple_recip_application.Dtos;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.Store.Actions;
using simple_recip_application.Resources;
using simple_recip_application.Services;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.Importation.Services;

public class RecipesFromPictureStrategy
(
    IServiceProvider _serviceProvider,
    IDispatcher _dispatcher
)
: IImportStrategy
{
    public async Task<MethodResult> ImportDataAsync(byte[] fileContent)
    {
        try
        {
            var _services = _serviceProvider.GetRequiredService<IOpenAiDataAnalysisService>();

            var recipe = await _services.ExtractRecipeFromImageAsync(fileContent);

            _dispatcher.Dispatch(new SetItemAction<IRecipeModel>(recipe));

            _dispatcher.Dispatch(new SetRecipeFormModalVisibilityAction(true));

            return new MethodResult(true, MessagesTranslator.ImportSuccess);
        }
        catch (Exception ex)
        {
            return new MethodResult(false, MessagesTranslator.ImportFailure);
        }
    }
}
