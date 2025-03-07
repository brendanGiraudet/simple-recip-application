using Microsoft.AspNetCore.Components;

namespace simple_recip_application.Components.SearchBar;

public partial class SearchBar : IDisposable
{
    [Parameter] public EventCallback<string> OnSearch { get; set; }
    [Parameter] public EventCallback<string> OnFocus { get; set; }
    [Parameter] public EventCallback<string> OnBlur { get; set; }

    private string SearchTerm { get; set; } = string.Empty;
    private Timer? _timer;

    private void OnSearchTermChanged(ChangeEventArgs e)
    {
        SearchTerm = e.Value?.ToString() ?? string.Empty;
        _timer?.Dispose();
        _timer = new Timer(OnTimerElapsed, null, 800, Timeout.Infinite);
    }

    private void OnTimerElapsed(object? state)
    {
        InvokeAsync(() => OnSearch.InvokeAsync(SearchTerm));
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    private void OnFocusHandler()
    {
        if(OnFocus.HasDelegate)
            InvokeAsync(() => OnFocus.InvokeAsync(SearchTerm));
    }

    private void OnBlurHandler()
    {
        if(OnBlur.HasDelegate)
            InvokeAsync(() => OnBlur.InvokeAsync(SearchTerm));
    }
}