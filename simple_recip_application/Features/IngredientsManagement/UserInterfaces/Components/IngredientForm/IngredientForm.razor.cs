using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore;
using simple_recip_application.Store.Actions;
using Microsoft.Extensions.Options;
using simple_recip_application.Settings;
using simple_recip_application.Features.NotificationsManagement.Persistence.Entites;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Enums;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore;
using Microsoft.Extensions.Localization;
using simple_recip_application.Resources;

namespace simple_recip_application.Features.IngredientsManagement.UserInterfaces.Components.IngredientForm;

public partial class IngredientForm
{
    [Parameter] public required IIngredientModel Ingredient { get; set; }
    [Parameter] public EventCallback<bool> OnCancel { get; set; }
    [Inject] public required IOptions<FileSettings> FileSettingsOptions { get; set; }
    [Inject] public required IStringLocalizer<Messages> MessagesStringLocalizer { get; set; }
    [Inject] public required ILogger<IngredientForm> Logger { get; set; }
    private FileSettings _fileSettings => FileSettingsOptions.Value;

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
            var notification = new NotificationMessage()
            {
                Message = MessagesStringLocalizer["MaxAllowedSizeError"],
                Type = NotificationType.Error
            };

            Dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

            return;
        }

        try
        {
            using var memoryStream = new MemoryStream();
            await file.OpenReadStream(_fileSettings.MaxAllowedSize).CopyToAsync(memoryStream);
            Ingredient.Image = memoryStream.ToArray();
        }
        catch (System.Exception ex)
        {
            Logger.LogError(ex, "Error while uploading image");
        }
    }

    protected void Submit()
    {
        if (Ingredient.Id.HasValue)
            Dispatcher.Dispatch(new UpdateItemAction<IIngredientModel>(Ingredient));

        else
            Dispatcher.Dispatch(new AddItemAction<IIngredientModel>(Ingredient));
    }

    private void DeleteIngredient(IIngredientModel model)
    {
        if (model.Id.HasValue)
            Dispatcher.Dispatch(new DeleteItemAction<IIngredientModel>(model));
    }

    private string GetDeleleButtonCssClass() => Ingredient.Id.HasValue ? "" : "hidden";
}