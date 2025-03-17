using simple_recip_application.Features.Importation.ApplicationCore.Entites;
using simple_recip_application.Features.Importation.Enums;

namespace simple_recip_application.Features.Importation.Store.Actions;

public record StartImportAction(ImportStrategyEnum ImportStrategy, IImportModel ImportModel);