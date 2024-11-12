using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.Ingredients;
using PersonalRecipeManagerWebApp.Models.RecipeApi;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask<bool> AddRecipeIngredientsAsync(RecipeIngredients ingredients)
        {
            bool isSuccessful = false;
            RecipeIngredients insertRecipeIngredients = await _broker.InsertRecipeIngredientsAsync(ingredients);
            if (insertRecipeIngredients is not null) isSuccessful = true;
            return isSuccessful;
        }
        public async ValueTask<List<RecipeIngredientsViewModel>> RetrieveRecipeIngredientsDtoByRecipeIdAsync(Guid id)
        {

            return await _broker.SelectRecipeIngredientsViewModelByRecipeIdAsync(id);
        }
        public async ValueTask<RecipeIngredients> RetrieveRecipeIngredientByIdAsync(Guid recipeId, Guid ingredientId)
        {
            return await _broker.SelectRecipeIngredientsByIdAsync(recipeId, ingredientId);
        }
        public async ValueTask<bool> UpsertRecipeIngredientAsync(Guid recipeId, RecipeIngredientsViewModel ingredient)
        {
            bool isSuccesssful = false;
            RecipeIngredients ingredientExists = await _broker.SelectRecipeIngredientsByIdAsync(recipeId, ingredient.Id);

            if (ingredientExists is null)
            {
                ingredientExists = new(recipeId, ingredient.Id, ingredient.Quantity, ingredient.UnitOfMeasurement);
                RecipeIngredients insertRecipeIngredients = await _broker.InsertRecipeIngredientsAsync(ingredientExists);
                if (insertRecipeIngredients is not null) isSuccesssful = true;
            }
            else
            {
                ingredientExists.IngredientId = ingredient.Id;
                ingredientExists.Quantity = ingredient.Quantity;
                RecipeIngredients updateRecipeIngredients = await _broker.UpdateRecipeIngredientsAsync(ingredientExists);
                if (updateRecipeIngredients is not null) isSuccesssful = true;
            }
            return isSuccesssful;
        }
        public async ValueTask<bool> RemoveRecipeIngredientAsync(Guid recipeId, RecipeIngredientsViewModel ingredient)
        {
            bool isSuccessful = false;
            RecipeIngredients editIngredients = await _broker.SelectRecipeIngredientsByIdAsync(recipeId, ingredient.Id);
            RecipeIngredients deleteRecipeIngredients = await _broker.DeleteRecipeIngredientsAsync(editIngredients);
            if (deleteRecipeIngredients is not null) isSuccessful = true;
            return isSuccessful;
        }
        public async ValueTask<bool> CheckIfRecipeHasIngredientByIdAsync(Guid recipeId, Guid ingredientId)
        {
            var hasIngredient = await _broker.SelectRecipeIngredientsByIdAsync(recipeId, ingredientId);
            return hasIngredient is not null;
        }
        public async ValueTask<bool> AddRecipeIngredientsFromMealDbAsync(MealsDbSearchCleaned selectedRecipe, Recipe recipe)
        {
            bool isSuccessful = false;
            try
            {
                _broker.BeginTransaction();
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
                isSuccessful = true;
                _broker.CommitTransaction();
            }
            catch
            {
                _broker.RollbackTransaction();
            }
            return isSuccessful;
        }


    }
}
