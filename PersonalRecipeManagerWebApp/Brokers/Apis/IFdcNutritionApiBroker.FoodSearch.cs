using PersonalRecipeManagerWebApp.Models.FoodDataApi;

namespace PersonalRecipeManagerWebApp.Brokers.Apis
{
    public partial interface IFdcNutritionApiBroker
    {
        ValueTask<List<SearchResultFood>> GetFoodListWithSimilarNameTo(string name);
    }
}
