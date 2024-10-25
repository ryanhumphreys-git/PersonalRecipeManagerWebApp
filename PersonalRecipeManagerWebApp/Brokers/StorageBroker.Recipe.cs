using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial class StorageBroker
    {
        public async ValueTask<Recipe> InsertRecipeAsync(Recipe recipe) =>
            await InsertAsync(recipe);
        public async ValueTask<List<Recipe>> SelectAllRecipeAsync() =>
            await SelectAllAsync<Recipe>();
        public async ValueTask<Recipe> SelectRecipeByIdAsync(Guid recipeId) =>
            await SelectAsync<Recipe>(recipeId);
        public async ValueTask<Recipe> UpdateRecipeAsync(Recipe recipe) =>
            await UpdateAsync(recipe);
        public async ValueTask<Recipe> DeleteRecipeAsync(Recipe recipe) =>
            await DeleteAsync(recipe);
    }
}
