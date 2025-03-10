using Microsoft.AspNetCore.Components;
using Microsoft.FeatureManagement;
using simple_recip_application.Constants;
using simple_recip_application.Resources;

namespace simple_recip_application.Components.Layout;

public partial class NavMenu
{
    [Inject] public required IFeatureManager FeatureManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await LoadMenuAsync();
    }

    private async Task LoadMenuAsync()
    {
        _navMenuItems = [ new NavMenuItem(string.Empty, "grocery", LabelsTranslator.Ingredients)];

        if(await FeatureManager.IsEnabledAsync(FeatureFlagsConstants.RecipeFeature))
            _navMenuItems = _navMenuItems.Append(new NavMenuItem("/recipes", "ramen_dining", LabelsTranslator.Recipes));
    }

    private IEnumerable<NavMenuItem> _navMenuItems = [];
}

public record class NavMenuItem(string Url, string Icon, string Text);