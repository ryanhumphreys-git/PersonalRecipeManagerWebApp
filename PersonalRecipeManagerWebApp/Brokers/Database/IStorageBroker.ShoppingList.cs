using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models.Ingredients;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial interface IStorageBroker
    {
        public ValueTask<UserShoppingList> InsertUserShoppingListAsync(UserShoppingList shoppingList);
        public ValueTask<List<UserShoppingList>> SelectAllUserShoppingListsAsync(Guid userId);
        public ValueTask<UserShoppingList> SelectShoppingListByIdAsync(Guid shoppingListId);
        public ValueTask<UserShoppingList> UpdateUserShoppingListAsync(UserShoppingList shoppingList);
        public ValueTask<UserShoppingList> SelectUserShoppingListByNameAsync(UserShoppingList shoppingList);
        public ValueTask<UserShoppingList> DeleteUserShoppingListAsync(UserShoppingList shoppingList);
        
    }
}
