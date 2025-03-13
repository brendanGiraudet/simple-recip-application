namespace simple_recip_application.Services;

public interface IOpenAiDataAnalysisService
{
    Task<string> ExtractTextFromImageAsync(byte[] imageData);
}