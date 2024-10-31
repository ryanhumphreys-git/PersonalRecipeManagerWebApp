using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial interface IStorageBroker
    {
        ValueTask<UserRecipes> InsertUserRecipeAsync(UserRecipes recipe);
        ValueTask<UserRecipes> SelectUserRecipesByIdAsync(Guid recipeId, Guid userId);
        ValueTask<List<UserRecipes>> SelectAllUserRecipesAsync(Guid userId);
        ValueTask DeleteUserRecipesByIdAsync(Guid recipeId);
    }
}
