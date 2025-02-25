using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using simple_recip_application.Resources;

namespace simple_recip_application.Components;

public class CustomComponentBase : ComponentBase
{
    [Inject] public required IDispatcher Dispatcher { get; set; }
    
    [Inject] public required IStringLocalizer<Labels> LabelsLocalizer { get; set; }

    [Parameter] public bool IsVisible { get; set; } = false;

    protected string _isVisibleCssClass => IsVisible ? string.Empty : "hidden";
}