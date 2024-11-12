using Microsoft.EntityFrameworkCore;
using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models.Ingredients;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial class StorageBroker
    {
        public async ValueTask<List<ShoppingListIngredients>> SelectShoppingListIngredientsByShoppingListIdAsync(Guid shoppingListId)
        {
            return await db.ShoppingListIngredients.Where(sli => sli.ShoppingListId == shoppingListId).ToListAsync();
        }
        public async ValueTask<ShoppingListIngredients> InsertShoppingListIngredientsAsync(ShoppingListIngredients ingredient)
        {
            return await InsertAsync(ingredient);
        }
        public async ValueTask<ShoppingListIngredients> DeleteShoppingListIngredientsAsync(ShoppingListIngredients ingredient)
        {
            return await DeleteAsync(ingredient);
        }
    }
}
