namespace PersonalRecipeManagerWebApp.Models;

public partial class KitchenType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Kitchen> Kitchens { get; set; } = new List<Kitchen>();
}
