using System.Text.Json;
using System.Text.Json.Serialization;

namespace PersonalRecipeManagerWebApp.JsonConverters
{
    public class MealDbIngredientConverter : JsonConverter<List<string>>
    {
        public override List<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var ingredientList = new List<string>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();
                    if (propertyName.StartsWith("strIngredient", StringComparison.OrdinalIgnoreCase))
                    {
                        reader.Read();
                        string ingredient = reader.GetString();
                        if (!string.IsNullOrEmpty(ingredient))
                        {
                            ingredientList.Add(ingredient);
                        }
                    }
                    else
                    {
                        reader.Skip();
                    }
                }
                else if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }
            }

            return ingredientList;
        }

        public override void Write(Utf8JsonWriter writer, List<string> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            for (int i = 0; i < value.Count; i++)
            {
                writer.WriteString($"strIngredient{i + 1}", value[i]);
            }
            writer.WriteEndObject();
        }
    }
}
