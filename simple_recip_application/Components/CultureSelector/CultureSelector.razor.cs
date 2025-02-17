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
        new("fr-FR"),
        new("en-US")
    };


    private CultureInfo _currentCultureInfo = CultureInfo.CurrentCulture;

    public CultureInfo CurrentCulture
    {
        get { return _currentCultureInfo; }
        set
        {
            if (_currentCultureInfo.TwoLetterISOLanguageName != value?.TwoLetterISOLanguageName)
            {
                _currentCultureInfo = value;

                var uri = new Uri(Navigation.Uri)
                    .GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);

                var cultureEscaped = Uri.EscapeDataString(_currentCultureInfo.Name);

                var uriEscaped = Uri.EscapeDataString(uri);

                Navigation.NavigateTo(
                    $"Culture/Set?culture={cultureEscaped}&redirectUri={uriEscaped}",
                    forceLoad: true);
            }
        }
    }

    protected bool IsSelected(string culture) => CultureInfo.CurrentCulture.TwoLetterISOLanguageName.Equals(culture, StringComparison.InvariantCultureIgnoreCase);

    protected void ChangeLanguage(ChangeEventArgs e)
    {
        var selectedLanguage = e.Value.ToString();
        if (selectedLanguage != null)
        {
            CurrentCulture = new CultureInfo(selectedLanguage);
        }
    }
}