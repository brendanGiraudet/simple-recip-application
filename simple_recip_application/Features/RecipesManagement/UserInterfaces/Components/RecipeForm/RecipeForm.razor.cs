using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using simple_recip_application.Constants;
using simple_recip_application.Features.Importation.Enums;
using simple_recip_application.Features.Importation.Store;
using simple_recip_application.Features.Importation.Store.Actions;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Enums;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.Store;
using simple_recip_application.Resources;
using simple_recip_application.Settings;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipesManagement.UserInterfaces.Components.RecipeForm;

public partial class RecipeForm
{
    [Inject] public required IFeatureManager FeatureManager { get; set; }
    [Inject] public required IState<RecipeState> RecipeState { get; set; }
    [Inject] public required IState<ImportState> ImportState { get; set; }
    [Inject] public required INotificationMessageFactory NotificationMessageFactory { get; set; }
    [Inject] public required ILogger<RecipeForm> Logger { get; set; }
    [Inject] public required IImportModelFactory ImportModelFactory { get; set; }
    [Inject] public required IOptions<FileSettings> FileSettingsOptions { get; set; }
    private FileSettings _fileSettings => FileSettingsOptions.Value;

    public IRecipeModel Recipe
    {
        get
        {
            return RecipeState.Value.Item;
        }
        set
        {
            Dispatcher.Dispatch(new SetItemAction<IRecipeModel>(value));
        }
    }
    [Parameter] public EventCallback<bool> OnCancel { get; set; }

    protected async Task OnCancelAsync()
    {
        if (OnCancel.HasDelegate)
            await OnCancel.InvokeAsync(false);
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
            Dispatcher.Dispatch(new SetLoadingAction<IRecipeModel>(true));
            using var memoryStream = new MemoryStream();

            await file.OpenReadStream(_fileSettings.MaxAllowedSize).CopyToAsync(memoryStream);

            Recipe.Image = memoryStream.ToArray();

            Dispatcher.Dispatch(new SetLoadingAction<IRecipeModel>(false));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error while handle image");
        }
    }

    protected void Submit()
    {
        if (Recipe.Id.HasValue)
            Dispatcher.Dispatch(new UpdateItemAction<IRecipeModel>(Recipe));
        else
            Dispatcher.Dispatch(new AddItemAction<IRecipeModel>(Recipe));
    }

    private void DeleteRecipe(IRecipeModel model)
    {
        if (model.Id.HasValue)
            Dispatcher.Dispatch(new DeleteItemAction<IRecipeModel>(model));
    }

    private string GetDeleteButtonCssClass() => Recipe.Id.HasValue ? "" : "hidden";

    protected async Task HandleImport(InputFileChangeEventArgs e)
    {
        Dispatcher.Dispatch(new SetLoadingAction<ImportStrategyEnum>(true));
        var file = e.File;
        if (file == null) return;

        if (file.Size > _fileSettings.MaxAllowedSize)
        {
            var notification = NotificationMessageFactory.CreateNotificationMessage(MessagesTranslator.MaxAllowedSizeError, NotificationType.Error);

            Dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

            Dispatcher.Dispatch(new SetLoadingAction<ImportStrategyEnum>(false));

            return;
        }

        try
        {
            using var memoryStream = new MemoryStream();

            await file.OpenReadStream(_fileSettings.MaxAllowedSize).CopyToAsync(memoryStream);

            var importModel = ImportModelFactory.CreateImportModel([memoryStream.ToArray()]);

            Dispatcher.Dispatch(new StartImportAction(ImportStrategyEnum.RecipesFromPicture, importModel));
        }
        catch (System.Exception ex)
        {
            Logger.LogError(ex, "Error while uploading image");
        }
    }

    private bool _isImportButtonVisible => !Recipe.Id.HasValue &&
                                           FeatureManager.IsEnabledAsync(FeatureFlagsConstants.RecipeImportationFeature).Result &&
                                           string.IsNullOrEmpty(Recipe.Name);
}