using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.RecipeApi;

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
            return await _broker.SelectRecipeIngredientsByIdAsync(recipeId, ingredientId);
        }
        public async ValueTask UpsertRecipeIngredientAsync(Guid recipeId, RecipeIngredientsViewModel ingredient)
        {
            RecipeIngredients ingredientExists = await _broker.SelectRecipeIngredientsByIdAsync(recipeId, ingredient.Id);

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
            RecipeIngredients editIngredients = await _broker.SelectRecipeIngredientsByIdAsync(recipeId, ingredient.Id);
            await _broker.DeleteRecipeIngredientsAsync(editIngredients);
        }
        public async ValueTask<bool> CheckIfRecipeHasIngredientByIdAsync(Guid recipeId, Guid ingredientId)
        {
            var hasIngredient = await _broker.SelectRecipeIngredientsByIdAsync(recipeId, ingredientId);
            return hasIngredient is not null;
        }
        public async ValueTask AddRecipeIngredientsFromMealDbAsync(MealsDbSearchCleaned selectedRecipe, Recipe recipe)
        {
            for (int i = 0; i < selectedRecipe.ingredients.Count(); i++)
            {
                Ingredients ingredient = await _broker.SelectWarehouseIngredientsByNameAsync(selectedRecipe.ingredients[i]);
                RecipeIngredients recipeIngredients = await RetrieveRecipeIngredientByIdAsync(recipe.Id, ingredient.Id);
                if (recipeIngredients == null)
                {
                    RecipeIngredients ingredients = new(recipe.Id, ingredient.Id, selectedRecipe.measurement[i], selectedRecipe.units[i]);
                    await AddRecipeIngredientsAsync(ingredients);
                }
            }
        }

    }
}
