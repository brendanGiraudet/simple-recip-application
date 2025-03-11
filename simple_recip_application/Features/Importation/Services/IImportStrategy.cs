namespace simple_recip_application.Features.Importation.Services;

public interface IImportStrategy
{
    Task<bool> ImportData(byte[] fileContent);
}