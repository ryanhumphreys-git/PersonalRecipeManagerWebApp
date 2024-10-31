using PersonalRecipeManagerWebApp.Models.RecipeApi;

namespace PersonalRecipeManagerWebApp.Services.ApiCallHandler
{
    public interface IHandleMealDbApi
    {
        ValueTask<List<MealsDbSearchCleaned>> GetMealDbSearchResultsByQueryExpression(string query);
    }
}
