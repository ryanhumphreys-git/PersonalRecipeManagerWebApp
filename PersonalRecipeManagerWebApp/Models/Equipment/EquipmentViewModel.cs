namespace PersonalRecipeManagerWebApp.Models.Equipment;

public class EquipmentViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Quantity { get; set; }
}

public class KitchenEquipmentViewModel : EquipmentViewModel
{
    public Guid KitchenId { get; set; }
}

public class RecipeEquipmentViewModel : EquipmentViewModel
{
    public Guid RecipeId { get; set; }
}