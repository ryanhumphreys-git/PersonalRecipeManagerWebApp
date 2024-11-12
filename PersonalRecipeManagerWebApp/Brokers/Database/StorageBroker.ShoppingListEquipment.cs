using Microsoft.EntityFrameworkCore;
using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models.Ingredients;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial class StorageBroker
    {
        public async ValueTask<List<ShoppingListEquipment>> SelectShoppingListEquipmentByShoppingListIdAsync(Guid shoppingListId)
        {
            return await db.ShoppingListEquipment.Where(sli => sli.ShoppingListId == shoppingListId).ToListAsync();
        }
        public async ValueTask<ShoppingListEquipment> InsertShoppingListEquipmentAsync(ShoppingListEquipment equipment)
        {
            return await InsertAsync(equipment);
        }
        public async ValueTask<ShoppingListEquipment> DeleteShoppingListEquipmentAsync(ShoppingListEquipment equipment)
        {
            return await DeleteAsync(equipment);
        }
    }
}
