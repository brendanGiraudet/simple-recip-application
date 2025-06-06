using simple_recip_application.Dtos;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;

namespace simple_recip_application.Services;

public interface IOpenAiDataAnalysisService
{
    Task<string> ExtractTextFromImageAsync(byte[] imageData);
    Task<MethodResult<IRecipeModel?>> ExtractRecipeFromImageAsync(byte[] imageData);
}