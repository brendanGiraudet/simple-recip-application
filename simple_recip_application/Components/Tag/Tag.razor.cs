using Microsoft.AspNetCore.Components;

namespace simple_recip_application.Components.Tag;

public partial class Tag
{
    [Parameter] public required RenderFragment ChildContent { get; set; }
    [Parameter] public EventCallback OnRemoveClick { get; set; }
    [Parameter] public EventCallback OnClick { get; set; }

    private async Task HandleRemoveClickAsync()
    {
        if (OnRemoveClick.HasDelegate)
        {
            await OnRemoveClick.InvokeAsync();
        }
    }

    private async Task HandleOnClickAsync()
    {
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync();
        }
    }
}
