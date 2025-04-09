using simple_recip_application.Features.Importation.Enums;

namespace simple_recip_application.Features.Importation.ApplicationCore.Entites;

public interface IImportModel
{
    public List<byte[]> FilesContent { get; set; }
    public ImportStrategyEnum? ImportStrategy { get; set; }
}