using Fluxor;
using Microsoft.AspNetCore.Components;

namespace simple_recip_application.Components;

public class CustomComponentBase : ComponentBase
{
    [Inject] public required IDispatcher Dispatcher { get; set; }

    [Parameter] public bool IsVisible { get; set; } = false;

    protected string _isVisibleCssClass => IsVisible ? string.Empty : "hidden";
}