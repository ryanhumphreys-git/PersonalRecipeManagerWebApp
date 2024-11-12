using Microsoft.EntityFrameworkCore;
using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models.Ingredients;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial class StorageBroker
    {
        public async ValueTask<UserShoppingList> InsertUserShoppingListAsync(UserShoppingList shoppingList) =>
            await InsertAsync(shoppingList);
        public async ValueTask<List<UserShoppingList>> SelectAllUserShoppingListsAsync(Guid userId)
        {
            return await db.UserShoppingList.Where(usl => usl.UserId == userId).ToListAsync();
        }
        public async ValueTask<UserShoppingList> SelectShoppingListByIdAsync(Guid shoppingListId) =>
            await SelectAsync<UserShoppingList>(shoppingListId);
        public async ValueTask<UserShoppingList> UpdateUserShoppingListAsync(UserShoppingList shoppingList) =>
            await UpdateAsync(shoppingList);
        public async ValueTask<UserShoppingList> DeleteUserShoppingListAsync(UserShoppingList shoppingList) =>
            await DeleteAsync(shoppingList);
        public async ValueTask<UserShoppingList> SelectUserShoppingListByNameAsync(UserShoppingList shoppingList)
        {
            return await db.UserShoppingList.Where(usl => usl.Name == shoppingList.Name && usl.UserId == shoppingList.UserId).FirstOrDefaultAsync();
        }
        
    }
}
