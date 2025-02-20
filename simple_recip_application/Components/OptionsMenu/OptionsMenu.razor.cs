using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using simple_recip_application.Resources;

namespace simple_recip_application.Components.OptionsMenu;

public partial class OptionsMenu
{
    [Inject] protected IStringLocalizer<Labels> LabelsLocalizer { get; set; } = default!;

    [Parameter] public List<OptionMenuItem> Options { get; set; } = new();

    private bool _isOptionsListVisible { get; set; } = false;

    private void ToggleOptionsList() =>_isOptionsListVisible = !_isOptionsListVisible;
}