using PersonalRecipeManagerWebApp.Models.Equipment;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask<List<ShoppingListEquipment>> RetrieveShoppingListEquipmentByShoppingListIdAsync(Guid shoppingListId)
        {
            return await _broker.SelectShoppingListEquipmentByShoppingListIdAsync(shoppingListId);
        }
        public async ValueTask<bool> AddShoppingListEquipmentAsync(ShoppingListEquipment equipment)
        {
            bool isSuccessful = false;
            ShoppingListEquipment addIngredient = await _broker.InsertShoppingListEquipmentAsync(equipment);
            if (addIngredient is not null) isSuccessful = true;
            return isSuccessful;
        }
        public async ValueTask<bool> RemoveShoppingListEquipmentAsync(ShoppingListEquipment equipment)
        {
            bool isSuccessful = false;
            ShoppingListEquipment removeIngredient = await _broker.DeleteShoppingListEquipmentAsync(equipment);
            if (removeIngredient is not null) isSuccessful = true;
            return isSuccessful;
        }
    }
}
