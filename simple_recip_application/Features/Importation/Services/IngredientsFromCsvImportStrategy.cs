namespace simple_recip_application.Features.Importation.Services;

public class IngredientsFromCsvImportStrategy
(
    CsvImportService _csvImportService
)
: IImportStrategy
{
    public async Task<bool> ImportData(Stream fileContent)
    {
        try
        {
            await _csvImportService.ImportIngredientsFromCsv(fileContent);
            
            return true;
        }
        catch
        {
            return false;
        }
    }
}
