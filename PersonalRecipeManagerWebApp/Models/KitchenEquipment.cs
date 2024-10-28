namespace PersonalRecipeManagerWebApp.Models;

public partial class KitchenEquipment
{
    public Guid KitchenId { get; set; }
    public Guid EquipmentId { get; set; }
    public double Quantity { get; set; }
    public virtual Kitchen? Kitchen { get; set; }
    public virtual Equipment? Equipment { get; set; }

    public KitchenEquipment() {}

    public KitchenEquipment(Guid kitchenId, Guid toolAndEquipmentId, double quantity)
    {
        KitchenId = kitchenId;
        EquipmentId = toolAndEquipmentId;
        Quantity = quantity;
    }
}
