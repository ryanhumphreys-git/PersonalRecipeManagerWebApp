using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask AddRecipeAsync(Recipe recipe)
        {
            await _broker.InsertRecipeAsync(recipe);
        }
        public async ValueTask<Recipe> RetrieveRecipeByIdAsync(Guid id)
        {

            return await _broker.SelectRecipeByIdAsync(id);
        }
        public async ValueTask UpsertRecipeAsync(Recipe recipe)
        {
            var recipeExists = await _broker.SelectRecipeByIdAsync(recipe.Id);

            if (recipeExists is null)
            {
                await _broker.InsertRecipeAsync(recipe);
            }
            else
            {
                await _broker.UpdateRecipeAsync(recipe);
            }
        }
        public async ValueTask RemoveRecipeAsync(Recipe recipe)
        {
            await _broker.DeleteRecipeAsync(recipe);
        }

    }
}
