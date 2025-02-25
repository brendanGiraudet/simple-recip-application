using Microsoft.AspNetCore.Components;

namespace simple_recip_application.Components.OptionsMenu;

public partial class OptionsMenu
{
    [Parameter] public List<OptionMenuItem> Options { get; set; } = new();

    private bool _isOptionsListVisible { get; set; } = false;

    private void ToggleOptionsList() =>_isOptionsListVisible = !_isOptionsListVisible;
}