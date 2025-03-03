using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using simple_recip_application.Features.Importation.Enums;
using simple_recip_application.Features.Importation.Store;
using simple_recip_application.Features.Importation.Store.Actions;
using simple_recip_application.Resources;

namespace simple_recip_application.Features.Importation.UserInterfaces.Pages;

public partial class ImportPage
{
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IState<ImportState> ImportState { get; set; }
    [Inject] public required IStringLocalizer<Labels> LabelsLocalizer { get; set; }

    // TODO pb avec le formulaire
    public ImportModel ImportModel { get; set; } = new()
    {
        FilePath = "/home/bakayarusama/Téléchargements/fr.openfoodfacts.org.products.csv"
    };

    private async Task HandleImport()
    {
        Dispatcher.Dispatch(new StartImportAction(ImportStrategyEnum.ImportIngredientsFromCsv, ImportModel));

        await Task.CompletedTask;
    }
}

public class ImportModel
{
    public string? FilePath { get; set; }
}