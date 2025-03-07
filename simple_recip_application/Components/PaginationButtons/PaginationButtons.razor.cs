using Microsoft.AspNetCore.Components;

namespace simple_recip_application.Components.PaginationButtons;

public partial class PaginationButtons
{
    [Parameter] public EventCallback OnPrevious { get; set; }
    [Parameter] public EventCallback OnNext { get; set; }
    [Parameter] public bool CanPreviousClick { get; set; }

    private async Task HandlePreviousClickAsync()
    {
        if (OnPrevious.HasDelegate)
            await OnPrevious.InvokeAsync();
    }

    private async Task HandleNextClickAsync()
    {
        if (OnNext.HasDelegate)
            await OnNext.InvokeAsync();
    }
}