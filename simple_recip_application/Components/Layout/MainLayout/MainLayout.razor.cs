using Microsoft.AspNetCore.Components;

namespace simple_recip_application.Components.Layout.MainLayout;

public partial class MainLayout
{
    [Inject] public required IConfiguration Configuration { get; set; }

    protected bool _isMenuOpen = false;

    protected void ToggleMenu()
    {
        _isMenuOpen = !_isMenuOpen;
    }
}
