using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models.Ingredients;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial interface IStorageBroker
    {
        public ValueTask<List<ShoppingListEquipment>> SelectShoppingListEquipmentByShoppingListIdAsync(Guid shoppingListId);
        public ValueTask<ShoppingListEquipment> InsertShoppingListEquipmentAsync(ShoppingListEquipment equipment);
        public ValueTask<ShoppingListEquipment> DeleteShoppingListEquipmentAsync(ShoppingListEquipment equipment);
    }
}
