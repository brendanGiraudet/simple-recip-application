using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;
using simple_recip_application.Features.Importation.ApplicationCore.Entites;
using simple_recip_application.Features.Importation.Enums;
using simple_recip_application.Features.Importation.Store;
using simple_recip_application.Features.Importation.Store.Actions;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Enums;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.RecipesManagement.Store;
using simple_recip_application.Features.RecipesManagement.Store.Actions;
using simple_recip_application.Resources;
using simple_recip_application.Settings;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.Importation.UserInterfaces.Pages.ImportPage;

public partial class ImportPage
{
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required IState<ImportState> ImportState { get; set; }
    [Inject] public required IState<RecipeState> RecipeState { get; set; }
    [Inject] public required ILogger<ImportPage> Logger { get; set; }
    [Inject] public required INotificationMessageFactory NotificationMessageFactory { get; set; }
    [Inject] public required IImportModelFactory ImportModelFactory { get; set; }
    [Inject] public required IOptions<FileSettings> FileSettingsOptions { get; set; }
    private FileSettings _fileSettings => FileSettingsOptions.Value;

    public IImportModel ImportModel { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        ImportModel = ImportModelFactory.CreateImportModel();
    }

    private async Task HandleImport()
    {
        Dispatcher.Dispatch(new StartImportAction(ImportStrategyEnum.RecipesFromHelloFreshPicture, ImportModel));

        await Task.CompletedTask;
    }

    protected async Task HandleImageUpload(InputFileChangeEventArgs e)
    {
        var file = e.File;
        if (file == null) return;

        if (file.Size > _fileSettings.MaxAllowedSize)
        {
            var notification = NotificationMessageFactory.CreateNotificationMessage(MessagesTranslator.MaxAllowedSizeError, NotificationType.Error);

            Dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

            return;
        }

        try
        {
            using var memoryStream = new MemoryStream();
            
            await file.OpenReadStream(_fileSettings.MaxAllowedSize).CopyToAsync(memoryStream);
            
            ImportModel.FileContent = memoryStream.ToArray();
        }
        catch (System.Exception ex)
        {
            Logger.LogError(ex, "Error while uploading image");
        }
    }

    private void CloseRecipeFormModal(bool isUpdated) => Dispatcher.Dispatch(new SetRecipeFormModalVisibilityAction(false));
}