using Microsoft.EntityFrameworkCore.SqlServer.Update.Internal;
using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.Ingredients;
using PersonalRecipeManagerWebApp.Models.RecipeApi;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask<bool> AddWarehouseIngredientsAsync(Ingredients ingredients)
        {
            bool isSuccessful = false;
            Ingredients insertWarehouseIngredients = await _broker.InsertWarehouseIngredientsAsync(ingredients);
            if (insertWarehouseIngredients is not null) isSuccessful = true;
            return isSuccessful;
        }
        public async ValueTask<List<Ingredients>> RetrieveAllWarehouseIngredientsAsync()
        {
            return await _broker.SelectAllWarehouseIngredientsAsync();
        }
        public async ValueTask<Ingredients> RetrieveWarehouseIngredientByIdAsync(Guid id)
        {
            return await _broker.SelectWarehouseIngredientsByIdAsync(id);
        }
        public async ValueTask<List<Recipe>> RetrieveRecipeCost(List<Recipe> recipes)
        {
            foreach (var recipe in recipes)
            {
                recipe.Cost = 0;
                List<RecipeIngredientsViewModel> recipeItems = await RetrieveRecipeIngredientsDtoByRecipeIdAsync(recipe.Id);
                foreach (var recipeItem in recipeItems)
                {
                    Ingredients currentIngredient = await _broker.SelectWarehouseIngredientsByIdAsync(recipeItem.Id);
                    recipe.Cost += currentIngredient.Cost;
                }
            }
            return recipes;
        }
        public async ValueTask<bool> UpsertWarehouseIngredientsAsync(Ingredients ingredients)
        {
            bool isSuccessful = false;
            var ingredientExists = await _broker.SelectWarehouseIngredientsByIdAsync(ingredients.Id);
            if (ingredientExists is null)
            {
                Ingredients insertWarehouseIngredients = await _broker.InsertWarehouseIngredientsAsync(ingredients);
                if (insertWarehouseIngredients is not null) isSuccessful = true;
            }
            else
            {
                Ingredients updateWarehouseIngredients = await _broker.UpdateWarehouseIngredientsAsync(ingredients);
                if (updateWarehouseIngredients is not null) isSuccessful = true;
            }
            return isSuccessful;
        }
        public async ValueTask<bool> RemoveWarehouseIngredientsAsync(Ingredients ingredients)
        {
            bool isSuccessful = false;
            try
            {
                Ingredients deleteWarehouseIngredients = await _broker.DeleteWarehouseIngredientsAsync(ingredients);
                if (deleteWarehouseIngredients is not null) isSuccessful = true;
            }
            catch
            {
                isSuccessful = false;
            }
            
            return isSuccessful;
        }
        public async ValueTask<bool> AddWarehouseIngredientFromMealDbAsync(MealsDbSearchCleaned selectedRecipe)
        {
            bool isSuccessful = false;
            try
            {
                _broker.BeginTransaction();
                for (int i = 0; i < selectedRecipe.ingredients.Count(); i++)
                {
                    Ingredients ingredient = await _broker.SelectWarehouseIngredientsByNameAsync(selectedRecipe.ingredients[i]);
                    if (ingredient == null)
                    {
                        Ingredients ingredients = new(Guid.NewGuid(), selectedRecipe.ingredients[i], 0, selectedRecipe.units[i]);
                        await _broker.InsertWarehouseIngredientsAsync(ingredients);
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
