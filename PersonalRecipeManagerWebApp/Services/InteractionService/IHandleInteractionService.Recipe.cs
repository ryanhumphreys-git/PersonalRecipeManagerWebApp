using PersonalRecipeManagerWebApp.Brokers.Apis;
using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.RecipeApi;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        ValueTask<bool> AddRecipeAsync(Recipe recipe);
        ValueTask<Recipe> RetrieveRecipeByIdAsync(Guid id);
        ValueTask<bool> UpsertRecipeAsync(Recipe recipe);
        ValueTask<bool> RemoveRecipeAsync(Recipe recipe);
        Recipe ConvertMealDbRecipeIntoRecipeType(MealsDbSearchCleaned mealDbRecipe);
        ValueTask<Recipe> AddRecipeFromMealDb(IList<MealsDbSearchCleaned> mealDbSearchResults);
        ValueTask<List<Recipe>> RetrieveAllUserRecipesByUserIdAsync(Guid userId);
        ValueTask<bool> CheckIfRecipeExistsByName(string name);
    }
}
