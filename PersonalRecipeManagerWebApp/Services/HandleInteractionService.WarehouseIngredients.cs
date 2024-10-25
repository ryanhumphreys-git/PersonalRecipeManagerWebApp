using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask AddWarehouseIngredientsAsync(Ingredients ingredients)
        {

            await _broker.InsertWarehouseIngredientsAsync(ingredients);
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
        public async ValueTask UpsertWarehouseIngredientsAsync(Ingredients ingredients)
        {

            var ingredientExists = await _broker.SelectWarehouseIngredientsByIdAsync(ingredients.Id);
            if (ingredientExists is null)
            {
                await _broker.InsertWarehouseIngredientsAsync(ingredients);
            }
            else
            {
                await _broker.UpdateWarehouseIngredientsAsync(ingredients);
            }
        }
        public async ValueTask RemoveWarehouseIngredientsAsync(Ingredients ingredients)
        {
            await _broker.DeleteWarehouseIngredientsAsync(ingredients);
        }

    }
}
