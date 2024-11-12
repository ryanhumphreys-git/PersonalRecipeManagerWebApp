namespace PersonalRecipeManagerWebApp.Models.Ingredients;

public partial class KitchenIngredients
{
    public Guid KitchenId { get; set; }
    public Guid IngredientId { get; set; }
    public double Quantity { get; set; }
    public virtual Ingredients? Ingredient { get; set; }
    public virtual Kitchen? Kitchen { get; set; }

    public KitchenIngredients(Guid kitchenId, Guid ingredientId, double quantity)
    {
        KitchenId = kitchenId;
        IngredientId = ingredientId;
        Quantity = quantity;
    }
}
