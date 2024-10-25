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
    }
}
