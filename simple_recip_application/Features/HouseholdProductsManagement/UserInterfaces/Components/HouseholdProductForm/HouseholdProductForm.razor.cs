using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Store.Actions;
using Microsoft.Extensions.Options;
using simple_recip_application.Settings;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Enums;
using simple_recip_application.Resources;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.HouseholdProductsManagement.UserInterfaces.Components.HouseholdProductForm;

public partial class HouseholdProductForm
{
    [Parameter] public required IHouseholdProductModel Product { get; set; }
    [Parameter] public EventCallback<bool> OnCancel { get; set; }
    [Inject] public required IOptions<FileSettings> FileSettingsOptions { get; set; }
    [Inject] public required ILogger<HouseholdProductForm> Logger { get; set; }
    [Inject] public required INotificationMessageFactory NotificationMessageFactory { get; set; }
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
            var notification = NotificationMessageFactory.CreateNotificationMessage(MessagesTranslator.MaxAllowedSizeError, NotificationType.Error);

            Dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));

            return;
        }

        try
        {
            using var memoryStream = new MemoryStream();
            await file.OpenReadStream(_fileSettings.MaxAllowedSize).CopyToAsync(memoryStream);
            Product.Image = memoryStream.ToArray();
        }
        catch (System.Exception ex)
        {
            Logger.LogError(ex, "Error while uploading image");
        }
    }

    protected void Submit()
    {
        if (Product.Id.HasValue)
            Dispatcher.Dispatch(new UpdateItemAction<IHouseholdProductModel>(Product));

        else
            Dispatcher.Dispatch(new AddItemAction<IHouseholdProductModel>(Product));
    }

    private void DeleteProduct(IHouseholdProductModel model)
    {
        if (model.Id.HasValue)
            Dispatcher.Dispatch(new DeleteItemAction<IHouseholdProductModel>(model));
    }

    private string GetDeleleButtonCssClass() => Product.Id.HasValue ? "" : "hidden";
}