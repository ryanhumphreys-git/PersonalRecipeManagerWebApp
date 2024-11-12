namespace PersonalRecipeManagerWebApp.Models.Equipment
{
    public class ShoppingListEquipment
    {
        public Guid ShoppingListId { get; set; }
        public Guid EquipmentId { get; set; }
        public double Quantity { get; set; }
        public virtual Equipment? Equipment { get; set; }
        public virtual UserShoppingList? UserShoppingList { get; set; }

        public ShoppingListEquipment() { }

        public ShoppingListEquipment(Guid shoppingListId, Guid equipmentId, double quantity)
        {
            ShoppingListId = shoppingListId;
            EquipmentId = equipmentId;
            Quantity = quantity;
        }
    }
}
