using System.Text.Json;
using System.Text.Json.Serialization;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;

namespace simple_recip_application.Features.RecipesManagement.Converters;

public class IngredientModelJsonConverter : JsonConverter<IIngredientModel>
{
    public override IIngredientModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<IngredientModel>(ref reader, options)!;
    }

    public override void Write(Utf8JsonWriter writer, IIngredientModel value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (IngredientModel)value, options);
    }
}