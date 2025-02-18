using Microsoft.AspNetCore.Components;

namespace simple_recip_application.Components.Modal;

public class ModalBase : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public EventCallback<bool> OnClose { get; set; }

    protected void CloseModal()
    {
        if(OnClose.HasDelegate)
            OnClose.InvokeAsync(false);
    }

    protected void CloseOnOverlayClick()
    {
        CloseModal();
    }
}