using Microsoft.EntityFrameworkCore;
using PersonalRecipeManagerWebApp.Models;
using System.Reflection.Metadata.Ecma335;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial class StorageBroker
    {
        public async ValueTask<UserRecipes> InsertUserRecipeAsync(UserRecipes userRecipe) =>
            await InsertAsync(userRecipe);
        public async ValueTask<UserRecipes> SelectUserRecipesByIdAsync(Guid userRecipeId, Guid userId) =>
            await SelectAsync<UserRecipes>(userRecipeId, userId);
        public async ValueTask<List<UserRecipes>> SelectAllUserRecipesAsync(Guid userId)
        {
            return await db.UserRecipes.Where(ur => ur.UserId == userId).ToListAsync();
        }
        public async ValueTask<UserRecipes> DeleteUserRecipesByIdAsync(Guid recipeId)
        {
            var recipeToDel = await db.UserRecipes.Where(ur => ur.RecipeId == recipeId).SingleOrDefaultAsync();
            await DeleteAsync(recipeToDel);
            return recipeToDel;
        }
    }
}
