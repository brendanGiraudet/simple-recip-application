using System.Text.Json;
using System.Text.Json.Serialization;
using Bogus;

namespace simple_recip_application.Tests.Fakers;

public class OpenApiReturnFaker : Faker<OpenApiReturn>
{
    public OpenApiReturnFaker()
    {
        ChoiceOpenApiReturn choiceOpenApiReturn = new ChoiceOpenApiReturn()
        {
            Message = new MeesageOpenApiReturn()
            {
                Content = $"```json {JsonSerializer.Serialize(new RecipeModelFaker().Generate())}```"
            }
        };

        RuleFor(c => c.Choices, f => [choiceOpenApiReturn]);
    }
}

public class OpenApiReturn
{
    [JsonPropertyName("choices")]
    public ChoiceOpenApiReturn[] Choices { get; set; } = [];
}

public class ChoiceOpenApiReturn
{
    [JsonPropertyName("message")]
    public MeesageOpenApiReturn? Message { get; set; }
}
public class MeesageOpenApiReturn
{
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}