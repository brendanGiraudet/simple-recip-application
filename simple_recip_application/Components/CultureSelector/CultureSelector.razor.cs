using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using simple_recip_application.Resources;
using System.Globalization;

namespace simple_recip_application.Components.CultureSelector;

public partial class CultureSelector
{
    [Inject] public required NavigationManager Navigation { get; set; }
    [Inject] public required IStringLocalizer<Labels> LabelsLocalizer { get; set; }

    private IEnumerable<CultureInfo> _supportedCultureInfos = new List<CultureInfo>()
    {
        new("fr-FR"),  // Français
        new("en-US")   // Anglais
    };

    private CultureInfo _selectedCulture;
    private bool _isDropdownVisible;

    protected override void OnInitialized()
    {
        _selectedCulture = CultureInfo.CurrentCulture;
    }

    // Méthode pour basculer la visibilité du dropdown
    private void ToggleDropdown()
    {
        _isDropdownVisible = !_isDropdownVisible;
    }

    // Sélectionner une culture
    private void SelectCulture(CultureInfo culture)
    {
        _selectedCulture = culture;
        _isDropdownVisible = false;
        ApplySelectedCultureAsync();
    }

    protected async Task ApplySelectedCultureAsync()
    {
        if (CultureInfo.CurrentCulture != _selectedCulture)
        {
            var uri = new Uri(Navigation.Uri)
                .GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
            var cultureEscaped = Uri.EscapeDataString(_selectedCulture.Name);
            var uriEscaped = Uri.EscapeDataString(uri);

            Navigation.NavigateTo(
                $"Culture/Set?culture={cultureEscaped}&redirectUri={uriEscaped}",
                forceLoad: true);
        }
    }

    // Obtenir le chemin de l'image du drapeau
    protected string GetFlagImagePath(string cultureName)
    {
        return $"/images/flags/{cultureName.Substring(0, 2).ToLower()}.png"; // Remplacer par le bon format ou chemin de vos fichiers
    }
}
