using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models.Ingredients;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask<List<UserShoppingList>> RetrieveAllUserShoppingListsAsync(Guid userId);
        public ValueTask<bool> AddUserShoppingListAsync(UserShoppingList shoppingList);
        public ValueTask<bool> UpsertUserShoppingListAsync(UserShoppingList shoppingList);
        public ValueTask<bool> RemoveUserShoppingListAsync(UserShoppingList shoppingList);
        public ValueTask<UserShoppingList> RetrieveShoppingListByShoppingListIdAsync(Guid shoppingListId);
    }  
}
