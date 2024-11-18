using Microsoft.AspNetCore.Http.Features;
using PersonalRecipeManagerWebApp.Models.Ingredients;

namespace PersonalRecipeManagerWebApp.Models;

public class ShoppingListViewModel
{
    // ingredients = 1, equipment = 2
    public int ShoppingListType { get; set; }
    public Guid ItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Quantity { get; set; }
    public bool IsSelected { get; set; } = false;

}