using System.Text.Json;
using System.Text.Json.Serialization;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;

namespace simple_recip_application.Features.RecipesManagement.Converters;

public class RecipeModelJsonConverter : JsonConverter<IRecipeModel>
{
    public override IRecipeModel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Désérialise vers la classe concrète
        var concreteRecipe = JsonSerializer.Deserialize<RecipeModel>(ref reader, options: options);
        return concreteRecipe;
    }

    public override void Write(Utf8JsonWriter writer, IRecipeModel value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (RecipeModel)value, options);
    }
}