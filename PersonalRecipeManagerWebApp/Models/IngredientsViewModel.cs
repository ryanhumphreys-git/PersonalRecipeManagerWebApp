namespace PersonalRecipeManagerWebApp.Models;

public class IngredientsViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Quantity { get; set; }
    public string UnitOfMeasurement { get; set; } = string.Empty;
}
public class KitchenIngredientsViewModel : IngredientsViewModel
{
   public Guid KitchenId { get; set; }
}

public class RecipeIngredientsViewModel : IngredientsViewModel
{
    public Guid RecipeId { get; set; }
}

