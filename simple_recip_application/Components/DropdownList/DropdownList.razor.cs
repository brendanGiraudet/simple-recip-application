using Microsoft.AspNetCore.Components;

namespace simple_recip_application.Components.DropdownList;

public partial class DropdownList<TItem>
{
    [Parameter] public EventCallback<string> OnSearch { get; set; }
    [Parameter] public EventCallback<TItem> OnClick { get; set; }
    [Parameter] public required IEnumerable<TItem> Items { get; set; } = [];
    [Parameter] public required string PropertyNameToDisplay { get; set; }

    private void OnSearchTermChanged(string searchTerm)
    {
        if (OnSearch.HasDelegate)
        {
            InvokeAsync(() => OnSearch.InvokeAsync(searchTerm));
        }
    }

    private void HandleOnClick(TItem item)
    {
        if (OnClick.HasDelegate)
        {
            InvokeAsync(() => OnClick.InvokeAsync(item));
        }

        _ddlVisibility = false;
    }

    private bool _ddlVisibility = false;

    private string DdlVisibilityCssClass => _ddlVisibility ? "visible" : "hidden";

    private bool _isMouseOverDropdown = false;
    private void OnMouseEnterDropdown() => _isMouseOverDropdown = true;

    private void OnMouseLeaveDropdown() => _isMouseOverDropdown = false;

    private async Task HideDropdownlist()
    {
        await Task.Delay(150);

        if (!_isMouseOverDropdown)
            _ddlVisibility = false;

        StateHasChanged();
    }

    private string GetOptionText(TItem item)
    {
        if (item is null || string.IsNullOrWhiteSpace(PropertyNameToDisplay))
            return item?.ToString() ?? string.Empty;

        var property = typeof(TItem).GetProperty(PropertyNameToDisplay)
                       ?? typeof(TItem).GetInterfaces()
                            .Select(i => i.GetProperty(PropertyNameToDisplay))
                            .FirstOrDefault(p => p is not null);

        return property?.GetValue(item)?.ToString() ?? string.Empty;
    }

}
