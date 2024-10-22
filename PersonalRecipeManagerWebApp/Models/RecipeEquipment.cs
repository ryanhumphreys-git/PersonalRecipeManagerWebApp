namespace PersonalRecipeManagerWebApp.Models;

public partial class RecipeEquipment
{
    public Guid AutoId { get; set; }
    public Guid RecipeId { get; set; }
    public Guid EquipmentId { get; set; }
    public double Quantity { get; set; }
    public virtual Recipe? Recipe { get; set; }
    public virtual Equipment? Equipment { get; set; }

    public RecipeEquipment(Guid autoId, Guid recipeId, Guid equipmentId, double quantity)
    {
        AutoId = autoId;
        RecipeId = recipeId;
        EquipmentId = equipmentId;
        Quantity = quantity;
    }
}
