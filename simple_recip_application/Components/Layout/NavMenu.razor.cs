using Microsoft.AspNetCore.Components;
using simple_recip_application.Resources;
using Microsoft.Extensions.Localization;

namespace simple_recip_application.Components.Layout;

public class NavMenuBase : ComponentBase
{
    [Inject] public required IStringLocalizer<Labels> LabelsLocalizer { get; set; }
}