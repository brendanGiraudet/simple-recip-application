using Microsoft.AspNetCore.Components;

namespace simple_recip_application.Components.Loader;

public partial class Loader
{
    [Parameter] public bool FillPage { get; set; } = true;
    public string _fillPageCssClass => FillPage ? "loader-fill-page" : string.Empty;
}