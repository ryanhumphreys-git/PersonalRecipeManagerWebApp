using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.Ingredients;
using PersonalRecipeManagerWebApp.Models.RecipeApi;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask<bool> AddRecipeIngredientsAsync(RecipeIngredients ingredients);
        public ValueTask<List<RecipeIngredientsViewModel>> RetrieveRecipeIngredientsDtoByRecipeIdAsync(Guid id);
        public ValueTask<RecipeIngredients> RetrieveRecipeIngredientByIdAsync(Guid recipeId, Guid ingredientId);
        public ValueTask<bool> UpsertRecipeIngredientAsync(Guid recipeId, RecipeIngredientsViewModel ingredient);
        public ValueTask<bool> RemoveRecipeIngredientAsync(Guid recipeId, RecipeIngredientsViewModel ingredient);
        public ValueTask<bool> CheckIfRecipeHasIngredientByIdAsync(Guid recipeId, Guid ingredientId);
        public ValueTask<bool> AddRecipeIngredientsFromMealDbAsync(MealsDbSearchCleaned selectedRecipe, Recipe recipe);

    }
}
