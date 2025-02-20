using Microsoft.AspNetCore.Components;

namespace simple_recip_application.Components.Loader;

public partial class Loader{
    [Parameter] public bool IsVisible { get; set; } = false;
    
    string _isVisibleCssClass => IsVisible ? string.Empty : "hidden";
}