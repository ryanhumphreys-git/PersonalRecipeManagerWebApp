namespace PersonalRecipeManagerWebApp.Models;

public class EquipmentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Quantity { get; set; }
}

public class KitchenEquipmentDto : EquipmentDto
{

}

public class RecipeEquipmentDto : EquipmentDto
{

}