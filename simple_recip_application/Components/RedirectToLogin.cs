using Microsoft.AspNetCore.Components;
using simple_recip_application.Constants;

namespace simple_recip_application.Components;

public class RedirectToLogin : ComponentBase
{
    [Inject] public required NavigationManager NavigationManager { get; set; }

    protected override void OnInitialized()
    {
        var returnUrl = Uri.EscapeDataString(new Uri(NavigationManager.Uri).PathAndQuery);
        NavigationManager.NavigateTo($"{PageUrlsConstants.Authentication}?returnUrl={returnUrl}", forceLoad: true);
    }
}