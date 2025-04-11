using Microsoft.AspNetCore.Components;

namespace simple_recip_application.Components.Icon;

public partial class Icon
{
    [Parameter]public string IconName { get; set; }
    [Parameter]public string Title { get; set; }
}
