using Microsoft.AspNetCore.Components;
using Microsoft.FeatureManagement;
using simple_recip_application.Constants;
using simple_recip_application.Resources;

namespace simple_recip_application.Components.Layout.NavMenu;

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
        _navMenuItems = [];

        if(await FeatureManager.IsEnabledAsync(FeatureFlagsConstants.RecipeFeature))
            _navMenuItems = _navMenuItems.Append(new NavMenuItem(PageUrlsConstants.RecipesPage, MaterialIconsConstants.RecipesPage, LabelsTranslator.Recipes));
        
        if(await FeatureManager.IsEnabledAsync(FeatureFlagsConstants.IngredientFeature))
            _navMenuItems = _navMenuItems.Append(new NavMenuItem(PageUrlsConstants.IngredientsPage, MaterialIconsConstants.IngredientsPage, LabelsTranslator.Ingredients));
        
        if(await FeatureManager.IsEnabledAsync(FeatureFlagsConstants.ImportationFeature))
            _navMenuItems = _navMenuItems.Append(new NavMenuItem(PageUrlsConstants.ImportationPage, MaterialIconsConstants.ImportationPage, LabelsTranslator.Importation));
        
        if(await FeatureManager.IsEnabledAsync(FeatureFlagsConstants.RecipePlanningFeature))
            _navMenuItems = _navMenuItems.Append(new NavMenuItem(PageUrlsConstants.RecipePlannigPage, MaterialIconsConstants.RecipePlannigPage, LabelsTranslator.Planning));
        
        if(await FeatureManager.IsEnabledAsync(FeatureFlagsConstants.PantryIngredientManagement))
            _navMenuItems = _navMenuItems.Append(new NavMenuItem(PageUrlsConstants.PantryIngredients, MaterialIconsConstants.PantryIngredients, LabelsTranslator.Pantry));
    }

    private IEnumerable<NavMenuItem> _navMenuItems = [];
}

public record class NavMenuItem(string Url, string Icon, string Text);