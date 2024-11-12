using Microsoft.AspNetCore.Http.Features;
using PersonalRecipeManagerWebApp.Models.Ingredients;

namespace PersonalRecipeManagerWebApp.Models;

public class ShoppingListViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<ShoppingListIngredients> ShoppingListIngredients { get; set; } = new();

    public ShoppingListViewModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}