namespace PersonalRecipeManagerWebApp.Models.Equipment;

public partial class RecipeEquipment
{
    public Guid RecipeId { get; set; }
    public Guid EquipmentId { get; set; }
    public double Quantity { get; set; }
    public virtual Recipe? Recipe { get; set; }
    public virtual Equipment? Equipment { get; set; }

    public RecipeEquipment(Guid recipeId, Guid equipmentId, double quantity)
    {
        RecipeId = recipeId;
        EquipmentId = equipmentId;
        Quantity = quantity;
    }
}
