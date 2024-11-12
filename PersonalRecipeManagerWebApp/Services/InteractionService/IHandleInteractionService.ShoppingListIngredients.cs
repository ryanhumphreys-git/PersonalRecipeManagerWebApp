using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models.Ingredients;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask<List<ShoppingListIngredients>> RetrieveShoppingListIngredientsByShoppingListIdAsync(Guid shoppingListId);
        public ValueTask<bool> AddShoppingListIngredientsAsync(ShoppingListIngredients ingredient);
        public ValueTask<bool> RemoveShoppingListIngredientAsync(ShoppingListIngredients ingredient);
    }
}

