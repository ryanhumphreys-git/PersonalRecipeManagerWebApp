using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalRecipeManagerWebApp.Models;
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public int CookingSkill { get; set; }
    public virtual ICollection<UserKitchen> UserKitchens { get; set; } = new List<UserKitchen>();
    public virtual ICollection<UserRecipes> UserRecipes { get; set; } = new List<UserRecipes>();
    [NotMapped]
    public ShoppingList UserShoppingList { get; set; } = new();
    [NotMapped]
    public List<Recipe> RecipeList { get; set; } = new();

    public User(Guid id, string name, int age, int cookingSkill)
    {
        Id = id;
        Name = name;
        Age = age;
        CookingSkill = cookingSkill;
    }
}