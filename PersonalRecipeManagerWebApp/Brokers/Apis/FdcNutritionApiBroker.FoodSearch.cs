using PersonalRecipeManagerWebApp.Models.FoodDataApi;

namespace PersonalRecipeManagerWebApp.Brokers.Apis
{
    public partial class FdcNutritionApiBroker
    {
        private readonly string foodSearchRelativeUrl = "v1/foods/search";
        public async ValueTask<List<SearchResultFood>> GetFoodListWithSimilarNameTo(string name) =>
            await this.GetAsync<List<SearchResultFood>>($"{foodSearchRelativeUrl}/api_key={ApiKey.fdc_nutrition_api_key}&query={name}");
    }
}
