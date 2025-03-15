using simple_recip_application.Dtos;

namespace simple_recip_application.Features.Importation.Services;

public class ImportService(IImportStrategy _importStrategy)
{
    public async Task<MethodResult> ExecuteImport(byte[] fileContent)
    {
        return await _importStrategy.ImportDataAsync(fileContent);
    }
}