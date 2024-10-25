using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask AddRecipeIngredientsAsync(RecipeIngredients ingredients);
        public ValueTask<List<RecipeIngredientsViewModel>> RetrieveRecipeIngredientsDtoByRecipeIdAsync(Guid id);
        public ValueTask<RecipeIngredients> RetrieveRecipeIngredientByIdAsync(Guid recipeId, Guid ingredientId);
        public ValueTask UpsertRecipeIngredientAsync(Guid recipeId, RecipeIngredientsViewModel ingredient);
        public ValueTask RemoveRecipeIngredientAsync(Guid recipeId, RecipeIngredientsViewModel ingredient);
        public ValueTask<bool> CheckIfRecipeHasIngredientByIdAsync(Guid recipeId, Guid ingredientId);

    }
}
