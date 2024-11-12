using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models.Ingredients;
using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask<List<ShoppingListEquipment>> RetrieveShoppingListEquipmentByShoppingListIdAsync(Guid shoppingListId);
        public ValueTask<bool> AddShoppingListEquipmentAsync(ShoppingListEquipment equipment);
        public ValueTask<bool> RemoveShoppingListEquipmentAsync(ShoppingListEquipment equipment);
    }
}
