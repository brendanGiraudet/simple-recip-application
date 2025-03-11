namespace simple_recip_application.Features.Importation.Services;

public class ImportService(IImportStrategy _importStrategy)
{
    public async Task<bool> ExecuteImport(byte[] fileContent)
    {
        return await _importStrategy.ImportData(fileContent);
    }
}