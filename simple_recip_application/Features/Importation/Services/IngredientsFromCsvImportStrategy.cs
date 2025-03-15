using simple_recip_application.Dtos;
using simple_recip_application.Resources;

namespace simple_recip_application.Features.Importation.Services;

public class IngredientsFromCsvImportStrategy
(
    IServiceProvider _serviceProvider
)
: IImportStrategy
{
    public async Task<MethodResult> ImportDataAsync(byte[] fileContent)
    {
        try
        {
            var _csvImportService = _serviceProvider.GetRequiredService<CsvImportService>();

            using var memoryStream = new MemoryStream(fileContent);
            
            await _csvImportService.ImportIngredientsFromCsv(memoryStream);
            
            return new MethodResult(true, MessagesTranslator.ImportSuccess);
        }
        catch(Exception ex)
        {
            return new MethodResult(true, MessagesTranslator.ImportSuccess);
        }
    }
}
