namespace simple_recip_application.Components.OptionsMenu;

public record OptionMenuItem(string Icon, string Text, Func<Task> OnClickAction, string? ToolTip = null);