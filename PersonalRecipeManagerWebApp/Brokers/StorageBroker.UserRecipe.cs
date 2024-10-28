using Microsoft.EntityFrameworkCore;
using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial class StorageBroker
    {
        public async ValueTask<UserRecipes> InsertUserRecipeAsync(UserRecipes userRecipe) =>
            await InsertAsync(userRecipe);
        public async ValueTask<List<UserRecipes>> SelectUserRecipesByIdAsync(Guid userRecipeId) =>
            await db.UserRecipes.Where(ur => ur.UserId == userRecipeId).ToListAsync();
        public async ValueTask DeleteUserRecipesByIdAsync(Guid recipeId)
        {
            var recipeToDel = await db.UserRecipes.Where(ur => ur.RecipeId == recipeId).SingleOrDefaultAsync();
            await DeleteAsync(recipeToDel);
        }
             
    }
}
