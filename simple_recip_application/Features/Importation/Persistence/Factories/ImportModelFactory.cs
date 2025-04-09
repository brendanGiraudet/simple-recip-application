using simple_recip_application.Features.Importation.ApplicationCore.Entites;
using simple_recip_application.Features.Importation.Persistence.Entites;

namespace simple_recip_application.Features.IngredientsManagement.ApplicationCore.Factories;

public class ImportModelFactory : IImportModelFactory
{
    public IImportModel CreateImportModel(List<byte[]>? FilesContent = null) => new ImportModel(){ FilesContent = FilesContent ?? [] };
}
