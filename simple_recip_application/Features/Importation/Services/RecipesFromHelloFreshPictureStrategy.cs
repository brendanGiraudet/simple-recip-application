using simple_recip_application.Services;

namespace simple_recip_application.Features.Importation.Services;

public class RecipesFromHelloFreshPictureStrategy
(
    IServiceProvider _serviceProvider
)
: IImportStrategy
{
    public async Task<bool> ImportData(byte[] fileContent)
    {
        try
        {
            var _tesseractServices =_serviceProvider.GetRequiredService<ITesseractServices>();

// TODO lire le contenu de l'image
// comprendre pourquoi on a une exception
            var text = _tesseractServices.ExtractTextFromImage(fileContent);
            
            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
    }
}
