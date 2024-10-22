namespace PersonalRecipeManagerWebApp.Models;

public class Equipment : Item
{
    public virtual ICollection<KitchenEquipment> KitchenEquipment { get; set; } = new List<KitchenEquipment>();
    public virtual ICollection<RecipeEquipment> RecipeEquipment { get; set; } = new List<RecipeEquipment>();

    public Equipment(Guid id)
        :base(id)
     { Id = id; }

    public Equipment(Guid id, string name, double cost)
        : base(id, name, cost)
    {

    }
}