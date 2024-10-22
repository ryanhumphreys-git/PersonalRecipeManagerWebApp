namespace PersonalRecipeManagerWebApp.Models;

public partial class UserRecipes
{
    public Guid AutoId { get; set; }

    public Guid? UserId { get; set; }

    public Guid? RecipeId { get; set; }

    public virtual User? User { get; set; }

    public virtual Recipe? Recipe { get; set; }

    public UserRecipes() { }

    public UserRecipes(Guid autoId, Guid userId, Guid recipeId)
    {
        AutoId = autoId;
        UserId = userId;
        RecipeId = recipeId;
    }
}
