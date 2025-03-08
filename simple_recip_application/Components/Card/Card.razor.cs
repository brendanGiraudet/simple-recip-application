using Microsoft.AspNetCore.Components;

namespace simple_recip_application.Components.Card;

public partial class Card<TContent>
{
    [Parameter] public TContent Item { get; set; } = default!;
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public string ImageSource { get; set; } = string.Empty;
    [Parameter] public EventCallback<TContent> OnClick { get; set; }
    [Parameter] public EventCallback<TContent> OnSelect { get; set; }
    [Parameter] public bool IsSelected { get; set; }
    [Parameter] public RenderFragment? BodyContent { get; set; }

    private string GetSelectionIconClass() => IsSelected ? "selected" : "not-selected";

    protected async Task OnClickAsync()
    {
        if (OnClick.HasDelegate)
            await OnClick.InvokeAsync(Item);
    }

    protected async Task OnSelectAsync()
    {
        if (OnSelect.HasDelegate)
            await OnSelect.InvokeAsync(Item);
    }
}