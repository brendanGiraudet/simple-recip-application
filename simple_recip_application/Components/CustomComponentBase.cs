using Microsoft.AspNetCore.Components;

namespace simple_recip_application.Components;

public class CustomComponentBase : ComponentBase
{
    [Parameter] public bool IsVisible { get; set; } = false;
    protected string _isVisibleCssClass => IsVisible ? string.Empty : "hidden";
}