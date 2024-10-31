using PersonalRecipeManagerWebApp.Models.FoodDataApi;

namespace PersonalRecipeManagerWebApp.Brokers.Apis
{
    public partial interface IFdcNutritionApiBroker
    {
        ValueTask<List<ListResultFood>> GetFoodListByPageNumberAndSize(int pageNumber, int pageSize);
    }
}
