using Microsoft.EntityFrameworkCore;
using PersonalRecipeManagerWebApp.Models;
using System.Runtime.InteropServices;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial class StorageBroker
    {
        public async ValueTask<RecipeIngredients> InsertRecipeIngredientsAsync(RecipeIngredients recipeIngredients) =>
            await InsertAsync(recipeIngredients);
        public async ValueTask<List<RecipeIngredients>> SelectAllRecipeIngredientsAsync() =>
            await SelectAllAsync<RecipeIngredients>();
        public async ValueTask<RecipeIngredients> SelectRecipeIngredientsByIdAsync(Guid[] ids) =>
            await SelectAsync<RecipeIngredients>(ids);
        public async ValueTask<RecipeIngredients> UpdateRecipeIngredientsAsync(RecipeIngredients recipeIngredients) =>
            await UpdateAsync(recipeIngredients);
        public async ValueTask<RecipeIngredients> DeleteRecipeIngredientsAsync(RecipeIngredients recipeIngredients) =>
            await DeleteAsync(recipeIngredients);
        public async ValueTask<List<RecipeIngredientsViewModel>> SelectRecipeIngredientsViewModelByRecipeIdAsync(Guid id)
        {

            return await db.Recipes
                .Where(r => r.Id == id)
                .SelectMany(r => r.RecipeIngredients)
                .Select(r => new RecipeIngredientsViewModel
                {
                    Id = r.Ingredient!.Id,
                    Name = r.Ingredient.Name,
                    Quantity = r.Quantity,
                    UnitOfMeasurement = r.Ingredient.UnitOfMeasurement
                })
                .ToListAsync();
        }
    }
}
