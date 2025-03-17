using simple_recip_application.Features.Importation.ApplicationCore.Entites;

namespace simple_recip_application.Features.IngredientsManagement.ApplicationCore.Factories;

public interface IImportModelFactory
{
    IImportModel CreateImportModel(byte[]? FileContent = null);
}
