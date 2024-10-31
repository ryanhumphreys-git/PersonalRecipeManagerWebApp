namespace PersonalRecipeManagerWebApp.Models;

public partial class UserRecipes
{
    public Guid? UserId { get; set; }

    public Guid? RecipeId { get; set; }

    public virtual User? User { get; set; }

    public virtual Recipe? Recipe { get; set; }

    public UserRecipes() { }

    public UserRecipes(Guid userId, Guid recipeId)
    {
        UserId = userId;
        RecipeId = recipeId;
    }
}
