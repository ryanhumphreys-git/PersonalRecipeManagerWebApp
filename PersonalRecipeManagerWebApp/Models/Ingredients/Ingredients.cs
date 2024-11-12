using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Antiforgery;

namespace PersonalRecipeManagerWebApp.Models.Ingredients;

public class Ingredients : Item
{
    [StringLength(50)]
    public string UnitOfMeasurement { get; set; }
    public virtual ICollection<KitchenIngredients> KitchenIngredients { get; set; } = new List<KitchenIngredients>();
    public virtual ICollection<RecipeIngredients> RecipeIngredients { get; set; } = new List<RecipeIngredients>();
    public virtual ICollection<ShoppingListIngredients> ShoppingListIngredients { get; set; } = new List<ShoppingListIngredients>();

    public Ingredients(Guid id)
        : base(id)
    { Id = id; }

    public Ingredients(Guid id, string name, double cost, string unitOfMeasurement)
        : base(id, name, cost)
    {
        UnitOfMeasurement = unitOfMeasurement;
    }
}