using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask AddRecipeIngredientsAsync(RecipeIngredients ingredients)
        {
            await _broker.InsertRecipeIngredientsAsync(ingredients);
        }
        public async ValueTask<List<RecipeIngredientsViewModel>> RetrieveRecipeIngredientsDtoByRecipeIdAsync(Guid id)
        {

            return await _broker.SelectRecipeIngredientsViewModelByRecipeIdAsync(id);
        }
        public async ValueTask<RecipeIngredients> RetrieveRecipeIngredientByIdAsync(Guid recipeId, Guid ingredientId)
        {
            Guid[] ids = { recipeId, ingredientId };
            return await _broker.SelectRecipeIngredientsByIdAsync(ids);
        }
        public async ValueTask UpsertRecipeIngredientAsync(Guid recipeId, RecipeIngredientsViewModel ingredient)
        {
            Guid[] ids = { recipeId, ingredient.Id };
            RecipeIngredients ingredientExists = await _broker.SelectRecipeIngredientsByIdAsync(ids);

            if (ingredientExists is null)
            {
                await _broker.InsertRecipeIngredientsAsync(ingredientExists);
            }
            else
            {
                await _broker.UpdateRecipeIngredientsAsync(ingredientExists);
            }
        }
        public async ValueTask RemoveRecipeIngredientAsync(Guid recipeId, RecipeIngredientsViewModel ingredient)
        {
            Guid[] ids = { recipeId, ingredient.Id };
            RecipeIngredients editIngredients = await _broker.SelectRecipeIngredientsByIdAsync(ids);
            await _broker.DeleteRecipeIngredientsAsync(editIngredients);
        }
        public async ValueTask<bool> CheckIfRecipeHasIngredientByIdAsync(Guid recipeId, Guid ingredientId)
        {
            Guid[] ids = { recipeId, ingredientId };
            var hasIngredient = await _broker.SelectRecipeIngredientsByIdAsync(ids);
            return hasIngredient is not null;
        }

    }
}
