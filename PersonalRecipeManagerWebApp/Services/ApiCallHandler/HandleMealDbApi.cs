using PersonalRecipeManagerWebApp.Brokers.Apis;
using PersonalRecipeManagerWebApp.Models.RecipeApi;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PersonalRecipeManagerWebApp.Services.ApiCallHandler
{
    public class HandleMealDbApi : IHandleMealDbApi
    {
        private readonly IMealDbRecipeBroker apibroker;

        public HandleMealDbApi(IMealDbRecipeBroker apibroker)
        {
            this.apibroker = apibroker;
        }

        public async ValueTask<List<MealsDbSearchCleaned>> GetMealDbSearchResultsByQueryExpression(string query)
        {
            MealsDbSearchResultMeals newResult = await this.apibroker.GetRecipeSearchResultByName(query);
            List<MealDbSearchResult> extractedResult = newResult.meals;
            if (extractedResult is null)
            {
                throw new Exception("No results");
            }
            List<MealsDbSearchCleaned> cleanResult = new List<MealsDbSearchCleaned>();
            foreach(MealDbSearchResult result in extractedResult)
            {
                MealsDbSearchCleaned mealToAdd = new();
                List<string> mealIngredients = new();
                List<double> mealMeasurements = new();
                List<string> mealUnits = new();
                mealToAdd.Name = result.strMeal;
                mealToAdd.Id = result.idMeal;
                mealToAdd.Category = result.strCategory;
                mealToAdd.Region = result.strArea;
                mealToAdd.Instructions = result.strInstructions;

                for (int i = 0; i <= 20;  i++)
                {
                    PropertyInfo prop = result.GetType().GetProperty($"strIngredient{i}");
                    if (prop is not null)
                    {
                        string value = (string)prop.GetValue(result);
                        if (!string.IsNullOrEmpty(value))
                        {
                            mealIngredients.Add(value);
                        }
                    }
                }

                for (int i = 0; i <= 20; i++)
                {
                    PropertyInfo prop = result.GetType().GetProperty($"strMeasure{i}");
                    if (prop is not null)
                    {
                        string value = (string)prop.GetValue(result);
                        if (!string.IsNullOrEmpty(value))
                        {
                            var match = Regex.Match(value, @"(\d+(\.\d+)?)(\s*[a-zA-Z]+)");

                            if (match.Success)
                            {
                                var measurement = match.Groups[1].Value;
                                var units = match.Groups[3].Value;
                                if (string.IsNullOrEmpty(units))
                                {
                                    units = "units";
                                }
                                mealMeasurements.Add(Convert.ToDouble(measurement));
                                mealUnits.Add(units);
                            }
                            else
                            {
                                var measurement = 1;
                                var units = "units";
                                mealMeasurements.Add(measurement);
                                mealUnits.Add(units);
                            }
                        }
                    }
                }

                mealToAdd.ingredients = mealIngredients;
                mealToAdd.measurement = mealMeasurements;
                mealToAdd.units = mealUnits;
                cleanResult.Add(mealToAdd);
            }

            return cleanResult;
        }
    }
}
