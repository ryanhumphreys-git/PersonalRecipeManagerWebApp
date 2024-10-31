using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial interface IStorageBroker
    {
        ValueTask<Recipe> InsertRecipeAsync(Recipe recipe);
        ValueTask<List<Recipe>> SelectAllRecipeAsync();
        ValueTask<Recipe> SelectRecipeByIdAsync(Guid recipeId);
        ValueTask<Recipe> UpdateRecipeAsync(Recipe recipe);
        ValueTask<Recipe> DeleteRecipeAsync(Recipe recipe);
        ValueTask<Recipe> SelectRecipeByNameAsync(string name);
    }
}
