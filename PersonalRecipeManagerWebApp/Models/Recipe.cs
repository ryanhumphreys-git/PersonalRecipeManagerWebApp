using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalRecipeManagerWebApp.Models;

public partial class Recipe
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Difficulty { get; set; }
    public double Time { get; set; }
    [NotMapped]
    public double Cost { get; set; }
    public virtual ICollection<UserRecipes> UserRecipes { get; set; } = new List<UserRecipes>();
    public virtual ICollection<RecipeIngredients> RecipeIngredients { get; set; } = new List<RecipeIngredients>();
    public virtual ICollection<RecipeEquipment> RecipeEquipment { get; set; } = new List<RecipeEquipment>();
    
    public Recipe(Guid id)
    {
        Id = id;
    }
    public Recipe(Guid id, string name, int difficulty, double time)
    {
        Id = id;
        Name = name;
        Difficulty = difficulty;
        Time = time;
    }
}
