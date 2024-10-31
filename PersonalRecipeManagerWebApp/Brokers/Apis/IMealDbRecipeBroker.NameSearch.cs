using PersonalRecipeManagerWebApp.Models.RecipeApi;

namespace PersonalRecipeManagerWebApp.Brokers.Apis
{
    public partial interface IMealDbRecipeBroker
    {
        ValueTask<MealsDbSearchResultMeals> GetRecipeSearchResultByName(string name);
    }
}
