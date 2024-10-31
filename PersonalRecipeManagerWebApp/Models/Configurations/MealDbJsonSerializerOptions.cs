using PersonalRecipeManagerWebApp.JsonConverters;
using System.Text.Json;

namespace PersonalRecipeManagerWebApp.Models.Configurations
{
    public class MealDbJsonSerializerOptions
    {
        public JsonSerializerOptions GetMealDbJsonSerializerOptions()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new MealDbIngredientConverter());
            options.Converters.Add(new MealDbMeasurementConverter());
            return options;
        }
    }
}
