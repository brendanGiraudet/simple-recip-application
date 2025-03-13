namespace simple_recip_application.Features.Importation.Services;

public class IngredientsFromCsvImportStrategy
(
    IServiceProvider _serviceProvider
)
: IImportStrategy
{
    public async Task<bool> ImportDataAsync(byte[] fileContent)
    {
        try
        {
            var _csvImportService = _serviceProvider.GetRequiredService<CsvImportService>();

            using var memoryStream = new MemoryStream(fileContent);
            
            await _csvImportService.ImportIngredientsFromCsv(memoryStream);
            
            return true;
        }
        catch
        {
            return false;
        }
    }
}
