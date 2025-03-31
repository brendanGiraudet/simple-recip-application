namespace simple_recip_application.Settings;

public class OpenApisettings
{
    public string ApiKey { get; set; } = string.Empty;
    public string ChatCompletionUrl { get; set; } = string.Empty;

    public int RetryCount { get; set; } = 3;
    public int RetryDelaySeconds { get; set; } = 2;
    public int MaxToken { get; set; } = 1000;
}