using Microsoft.AspNetCore.Components;
using Microsoft.FeatureManagement;
using simple_recip_application.Constants;
using simple_recip_application.Resources;

namespace simple_recip_application.Components.Layout.NavMenu;

public partial class NavMenu
{
    [Inject] public required IFeatureManager FeatureManager { get; set; }

    [Parameter] public EventCallback OnMenuItemClicked { get; set; }

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
        
        if(await FeatureManager.IsEnabledAsync(FeatureFlagsConstants.CalendarManagementFeature))
            _navMenuItems = _navMenuItems.Append(new NavMenuItem(PageUrlsConstants.CalendarsPage, MaterialIconsConstants.RecipePlannigPage, LabelsTranslator.Calendars));
        
        if(await FeatureManager.IsEnabledAsync(FeatureFlagsConstants.ImportationFeature))
            _navMenuItems = _navMenuItems.Append(new NavMenuItem(PageUrlsConstants.ImportationPage, MaterialIconsConstants.ImportationPage, LabelsTranslator.Importation));
        
        if(await FeatureManager.IsEnabledAsync(FeatureFlagsConstants.ShoppingListManagement))
            _navMenuItems = _navMenuItems.Append(new NavMenuItem(PageUrlsConstants.ShoppingList, MaterialIconsConstants.ShoppingList, LabelsTranslator.ShoppingList));
        
        if(await FeatureManager.IsEnabledAsync(FeatureFlagsConstants.ProductManagementFeature))
            _navMenuItems = _navMenuItems.Append(new NavMenuItem(PageUrlsConstants.HouseholdProducts, MaterialIconsConstants.Products, LabelsTranslator.Products));
        
        if(await FeatureManager.IsEnabledAsync(FeatureFlagsConstants.UserPantryManagement))
            _navMenuItems = _navMenuItems.Append(new NavMenuItem(PageUrlsConstants.UserPantry, MaterialIconsConstants.UserPantry, LabelsTranslator.Pantry));
        
        if(await FeatureManager.IsEnabledAsync(FeatureFlagsConstants.TagManagement))
            _navMenuItems = _navMenuItems.Append(new NavMenuItem(PageUrlsConstants.TagManagement, MaterialIconsConstants.TagManagement, LabelsTranslator.Tags));
    }

    private IEnumerable<NavMenuItem> _navMenuItems = [];
}

public record class NavMenuItem(string Url, string Icon, string Text);