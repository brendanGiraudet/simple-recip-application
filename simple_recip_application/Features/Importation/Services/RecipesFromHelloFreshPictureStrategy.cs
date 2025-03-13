using simple_recip_application.Services;

namespace simple_recip_application.Features.Importation.Services;

public class RecipesFromHelloFreshPictureStrategy
(
    IServiceProvider _serviceProvider
)
: IImportStrategy
{
    public async Task<bool> ImportDataAsync(byte[] fileContent)
    {
        try
        {
            var _services = _serviceProvider.GetRequiredService<IOpenAiDataAnalysisService>();

            var text = _services.ExtractTextFromImageAsync(fileContent);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
