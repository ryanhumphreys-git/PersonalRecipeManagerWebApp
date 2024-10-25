using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial interface IStorageBroker
    {
        ValueTask<KitchenIngredients> InsertKitchenIngredientAsync(KitchenIngredients ingredient);
        ValueTask<List<KitchenIngredients>> SelectAllKitchenIngredientsAsync();
        ValueTask<KitchenIngredients> SelectKitchenIngredientByIdAsync(Guid ingredientId);
        ValueTask<KitchenIngredients> UpdateKitchenIngredientAsync(KitchenIngredients ingredient);
        ValueTask<KitchenIngredients> DeleteKitchenIngredientsAsync(KitchenIngredients ingredient);
        ValueTask<List<KitchenIngredientsViewModel>> SelectKitchenIngredientsViewModelByKitchenIdAsync(Guid id);
    }
}
