using Microsoft.AspNetCore.Components;
using simple_recip_application.Resources;
using Microsoft.Extensions.Localization;

namespace simple_recip_application.Components.Layout;

public partial class NavMenu : ComponentBase
{
    [Inject] public required IStringLocalizer<Labels> LabelsLocalizer { get; set; }
}