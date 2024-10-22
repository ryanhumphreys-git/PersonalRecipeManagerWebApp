namespace PersonalRecipeManagerWebApp.Models;

public class IngredientsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Quantity { get; set; }
    public string UnitOfMeasurement { get; set; } = string.Empty;
}
public class KitchenIngredientsDto : IngredientsDto
{
   
}

public class RecipeIngredientsDto : IngredientsDto
{

}

