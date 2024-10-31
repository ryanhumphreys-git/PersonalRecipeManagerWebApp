using PersonalRecipeManagerWebApp.Models.FoodDataApi;

namespace PersonalRecipeManagerWebApp.Services.ApiCallHandler
{
    public partial interface IHandleFdcNutritionApi
    {
        ValueTask<List<SearchResultFood>> GetFoodSearchResultsByQueryExpression(string query);

        ValueTask<List<ListResultFood>> GetFoodListByPageNumberAndSize(int pageNumber, int pageSize);
    }
}
