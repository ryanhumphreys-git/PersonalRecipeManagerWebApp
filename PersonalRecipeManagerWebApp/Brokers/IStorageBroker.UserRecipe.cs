using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial interface IStorageBroker
    {
        ValueTask<UserRecipes> InsertUserRecipeAsync(UserRecipes recipe);
        ValueTask<List<UserRecipes>> SelectUserRecipesByIdAsync(Guid recipeId);
        ValueTask DeleteUserRecipesByIdAsync(Guid recipeId);
    }
}
