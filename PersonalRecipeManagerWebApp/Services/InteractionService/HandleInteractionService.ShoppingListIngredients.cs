using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.Ingredients;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask<List<ShoppingListIngredients>> RetrieveShoppingListIngredientsByShoppingListIdAsync(Guid shoppingListId)
        {
            return await _broker.SelectShoppingListIngredientsByShoppingListIdAsync(shoppingListId);
        }
        public async ValueTask<bool> AddShoppingListIngredientsAsync(ShoppingListIngredients ingredient)
        {
            bool isSuccessful = false;
            ShoppingListIngredients addIngredient = await _broker.InsertShoppingListIngredientsAsync(ingredient);
            if (addIngredient is not null) isSuccessful = true;
            return isSuccessful;
        }
        public async ValueTask<bool> RemoveShoppingListIngredientAsync(ShoppingListIngredients ingredient)
        {
            bool isSuccessful = false;
            ShoppingListIngredients removeIngredient = await _broker.DeleteShoppingListIngredientsAsync(ingredient);
            if (removeIngredient is not null) isSuccessful = true;
            return isSuccessful;
        }

    }
}
