namespace PersonalRecipeManagerWebApp.Models.Equipment;

public class Equipment : Item
{
    public virtual ICollection<KitchenEquipment> KitchenEquipment { get; set; } = new List<KitchenEquipment>();
    public virtual ICollection<RecipeEquipment> RecipeEquipment { get; set; } = new List<RecipeEquipment>();
    public virtual ICollection<ShoppingListEquipment> ShoppingListEquipments { get; set; } = new List<ShoppingListEquipment>();

    public Equipment(Guid id)
        : base(id)
    { Id = id; }

    public Equipment(Guid id, string name, double cost)
        : base(id, name, cost)
    {

    }
}