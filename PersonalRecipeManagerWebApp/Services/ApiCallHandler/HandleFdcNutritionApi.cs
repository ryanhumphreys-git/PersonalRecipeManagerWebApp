using PersonalRecipeManagerWebApp.Brokers.Apis;
using PersonalRecipeManagerWebApp.Models.FoodDataApi;

namespace PersonalRecipeManagerWebApp.Services.ApiCallHandler
{
    public partial class HandleFdcNutritionApi : IHandleFdcNutritionApi
    {
        private readonly IFdcNutritionApiBroker apibroker;

        public HandleFdcNutritionApi(IFdcNutritionApiBroker apibroker)
        {
            this.apibroker = apibroker;
        }

        public async ValueTask<List<SearchResultFood>> GetFoodSearchResultsByQueryExpression(string query)
        {
            return await this.apibroker.GetFoodListWithSimilarNameTo(query);
        }

        public async ValueTask<List<ListResultFood>> GetFoodListByPageNumberAndSize(int pageNumber, int pageSize)
        {
            return await this.apibroker.GetFoodListByPageNumberAndSize(pageNumber, pageSize);
        }
    }
}
