using simple_recip_application.Features.Importation.ApplicationCore.Entites;

namespace simple_recip_application.Features.Importation.Persistence.Entites;

public class ImportModel : IImportModel
{
    public byte[]? FileContent { get; set; }
}