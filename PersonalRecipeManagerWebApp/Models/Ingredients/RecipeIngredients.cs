namespace PersonalRecipeManagerWebApp.Models.Ingredients;

public partial class RecipeIngredients
{
    public Guid RecipeId { get; set; }
    public Guid IngredientId { get; set; }
    public double Quantity { get; set; }
    public string? UnitOfMeasurement { get; set; }
    public virtual Ingredients? Ingredient { get; set; }
    public virtual Recipe? Recipes { get; set; }


    public RecipeIngredients(Guid recipeId, Guid ingredientId, double quantity, string unitOfMeasurement)
    {
        RecipeId = recipeId;
        IngredientId = ingredientId;
        Quantity = quantity;
        UnitOfMeasurement = unitOfMeasurement;
    }
}
