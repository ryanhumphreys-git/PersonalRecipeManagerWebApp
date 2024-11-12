using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models.Ingredients;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial interface IStorageBroker
    {
        public ValueTask<List<ShoppingListIngredients>> SelectShoppingListIngredientsByShoppingListIdAsync(Guid shoppingListId);
        public ValueTask<ShoppingListIngredients> InsertShoppingListIngredientsAsync(ShoppingListIngredients ingredient);
        public ValueTask<ShoppingListIngredients> DeleteShoppingListIngredientsAsync(ShoppingListIngredients ingredient);
    }
}
