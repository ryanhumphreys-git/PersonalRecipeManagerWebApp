using PersonalRecipeManagerWebApp.Models.RecipeApi;

namespace PersonalRecipeManagerWebApp.Brokers.Apis
{
    public partial class MealDbRecipeBroker
    {
        private string mealDbSearchRelativeUrl = "search.php?s=";

        public async ValueTask<MealsDbSearchResultMeals> GetRecipeSearchResultByName(string name) =>
            await this.GetAsync<MealsDbSearchResultMeals>($"{mealDbSearchRelativeUrl}{name}");
    }
}
