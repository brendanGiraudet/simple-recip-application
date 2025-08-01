using System.Text.Json;
using System.Text.Json.Serialization;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entities;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;

namespace simple_recip_application.Features.RecipesManagement.Converters;

public class RecipeIngredientModelJsonConverter : JsonConverter<IRecipeIngredientModel>
{
    public override IRecipeIngredientModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<RecipeIngredientModel>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, IRecipeIngredientModel value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (RecipeIngredientModel)value, options);
    }
}