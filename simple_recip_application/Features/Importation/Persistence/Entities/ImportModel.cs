using simple_recip_application.Features.Importation.ApplicationCore.Entites;
using simple_recip_application.Features.Importation.Enums;

namespace simple_recip_application.Features.Importation.Persistence.Entites;

public class ImportModel : IImportModel
{
    public List<byte[]> FilesContent { get; set; } = [];
    public ImportStrategyEnum? ImportStrategy { get; set; }=  ImportStrategyEnum.RecipesFromPicture;
}