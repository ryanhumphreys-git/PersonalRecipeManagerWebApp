using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.Ingredients;
using PersonalRecipeManagerWebApp.Models.RecipeApi;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask<bool> AddWarehouseIngredientsAsync(Ingredients ingredients);
        public ValueTask<List<Ingredients>> RetrieveAllWarehouseIngredientsAsync();
        public ValueTask<Ingredients> RetrieveWarehouseIngredientByIdAsync(Guid id);
        public ValueTask<List<Recipe>> RetrieveRecipeCost(List<Recipe> recipes);
        public ValueTask<bool> UpsertWarehouseIngredientsAsync(Ingredients ingredients);
        public ValueTask<bool> RemoveWarehouseIngredientsAsync(Ingredients ingredient);
        public ValueTask<bool> AddWarehouseIngredientFromMealDbAsync(MealsDbSearchCleaned selectedRecipe);
    }
}
