namespace PersonalRecipeManagerWebApp.Models;

public partial class UserKitchen
{
    public Guid? UserId { get; set; }

    public Guid KitchenId { get; set; }

    public virtual User? User { get; set; }

    public virtual Kitchen? Kitchen { get; set; }
}
