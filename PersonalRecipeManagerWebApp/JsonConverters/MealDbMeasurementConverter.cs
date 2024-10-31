using System.Diagnostics.Metrics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PersonalRecipeManagerWebApp.JsonConverters
{
    public class MealDbMeasurementConverter : JsonConverter<List<string>>
    {
        public override List<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var measurementList = new List<string>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();
                    if (propertyName.StartsWith("strMeasure", StringComparison.OrdinalIgnoreCase))
                    {
                        reader.Read();
                        string measurement = reader.GetString();
                        if (!string.IsNullOrEmpty(measurement))
                        {
                            measurementList.Add(measurement);
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

            return measurementList;
        }

        public override void Write(Utf8JsonWriter writer, List<string> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            for (int i = 0; i < value.Count; i++)
            {
                writer.WriteString($"strMeasure{i + 1}", value[i]);
            }
            writer.WriteEndObject();
        }
    }
}
