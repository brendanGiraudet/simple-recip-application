using simple_recip_application.Features.Importation.Enums;
using simple_recip_application.Features.Importation.UserInterfaces.Pages;

namespace simple_recip_application.Features.Importation.Store.Actions;

public record StartImportAction(ImportStrategyEnum ImportStrategy, ImportModel ImportModel);