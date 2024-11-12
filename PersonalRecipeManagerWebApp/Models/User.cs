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
    public ICollection<UserShoppingList> UserShoppingLists { get; set; } = new List<UserShoppingList>();

    public User(Guid id, string name, int age, int cookingSkill)
    {
        Id = id;
        Name = name;
        Age = age;
        CookingSkill = cookingSkill;
    }
}