using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models.Ingredients;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask<List<UserShoppingList>> RetrieveAllUserShoppingListsAsync(Guid userId)
        {
            return await _broker.SelectAllUserShoppingListsAsync(userId);
        }
        public async ValueTask<UserShoppingList> RetrieveShoppingListByShoppingListIdAsync(Guid shoppingListId)
        {
            return await _broker.SelectShoppingListByIdAsync(shoppingListId);
        }
        public async ValueTask<bool> AddUserShoppingListAsync(UserShoppingList shoppingList)
        {
            bool isSuccessful = false;
            UserShoppingList insertShoppingList = await _broker.InsertUserShoppingListAsync(shoppingList);
            if (insertShoppingList is not null) isSuccessful = true;
            return isSuccessful;
        }
        public async ValueTask<bool> UpsertUserShoppingListAsync(UserShoppingList shoppingList)
        {
            bool isSuccessful = false;
            var shoppingListExists = await _broker.SelectRecipeByIdAsync(shoppingList.ShoppingListId);

            if (shoppingListExists is null)
            {
                UserShoppingList insertRecipe = await _broker.InsertUserShoppingListAsync(shoppingList);
                if (insertRecipe is not null) isSuccessful = true;
            }
            else
            {
                UserShoppingList updateRecipe = await _broker.UpdateUserShoppingListAsync(shoppingList);
                if (updateRecipe is not null) isSuccessful = true;
            }
            return isSuccessful;
        }
        public async ValueTask<bool> RemoveUserShoppingListAsync(UserShoppingList shoppingList)
        {
            bool isSuccessful = false;
            UserShoppingList deleteShoppingList = await _broker.DeleteUserShoppingListAsync(shoppingList);
            if (deleteShoppingList is null) return false;
            return isSuccessful;
        }
        
    }
}
