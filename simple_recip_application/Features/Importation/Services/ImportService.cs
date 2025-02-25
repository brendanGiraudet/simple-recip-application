namespace simple_recip_application.Features.Importation.Services;

public class ImportService(IImportStrategy _importStrategy)
{
    public async Task<bool> ExecuteImport(string filePath)
    {
        using var fileStream = File.OpenRead(filePath);

        return await _importStrategy.ImportData(fileStream);
    }
}