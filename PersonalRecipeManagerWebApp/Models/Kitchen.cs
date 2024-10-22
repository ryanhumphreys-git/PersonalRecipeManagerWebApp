namespace PersonalRecipeManagerWebApp.Models;

public class Kitchen
{
    public Guid Id { get; set; }
    public Guid? TypeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<UserKitchen> UserKitchens { get; set; } = new List<UserKitchen>();
    public ICollection<KitchenEquipment> KitchenEquipment { get; set; } = new List<KitchenEquipment>();
    public ICollection<KitchenIngredients> KitchenIngredients { get; set; } = new List<KitchenIngredients>();
    public KitchenType? Type { get; set; }

    public Kitchen() { }

    public Kitchen(Guid id, Guid? typeId, string name)
    { 
        Id = id;
        TypeId = typeId;
        Name = name;
    }
}