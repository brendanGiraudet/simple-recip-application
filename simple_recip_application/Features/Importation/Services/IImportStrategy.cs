using simple_recip_application.Dtos;

namespace simple_recip_application.Features.Importation.Services;

public interface IImportStrategy
{
    Task<MethodResult> ImportDataAsync(byte[] fileContent);
}