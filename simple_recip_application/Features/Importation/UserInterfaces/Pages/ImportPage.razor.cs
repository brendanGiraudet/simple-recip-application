using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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

    private ImportModel _importModel = new();

    private async Task HandleImport()
    {
        Dispatcher.Dispatch(new StartImportAction(ImportStrategyEnum.ImportIngredientsFromCsv));
    }

    private async Task HandleFileUpload(InputFileChangeEventArgs e)
    {
        var file = e.File;
        if (file != null)
        {
            using var memoryStream = new MemoryStream();
            
            await file.OpenReadStream().CopyToAsync(memoryStream);

            Dispatcher.Dispatch(new SetFileContentAction(memoryStream.ToArray()));
        }
    }
}

public class ImportModel
{
    public Stream? fileContent { get; set; }
}