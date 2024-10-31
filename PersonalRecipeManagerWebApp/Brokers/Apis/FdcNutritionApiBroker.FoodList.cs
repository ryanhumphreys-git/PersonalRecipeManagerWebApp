using PersonalRecipeManagerWebApp.Models.FoodDataApi;

namespace PersonalRecipeManagerWebApp.Brokers.Apis
{
    public partial class FdcNutritionApiBroker
    {
        private string foodListRelativeUrl = "v1/foods/list";

        public async ValueTask<List<ListResultFood>> GetFoodListByPageNumberAndSize(int pageNumber, int pageSize) =>
            await this.GetAsync<List<ListResultFood>>($"{foodListRelativeUrl}?datatype=branded&pageSize={pageSize}&pageNumber={pageNumber}&api_key={ApiKey.fdc_nutrition_api_key}");
    }
}
