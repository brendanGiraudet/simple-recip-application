using Microsoft.AspNetCore.Components;

namespace simple_recip_application.Components.Layout;

public partial class MainLayout
{
    [Inject] public required IConfiguration Configuration { get; set; }
}
