using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask AddWarehouseIngredientsAsync(Ingredients ingredients);
        public ValueTask<List<Ingredients>> RetrieveAllWarehouseIngredientsAsync();
        public ValueTask<Ingredients> RetrieveWarehouseIngredientByIdAsync(Guid id);
        public ValueTask<List<Recipe>> RetrieveRecipeCost(List<Recipe> recipes);
        public ValueTask UpsertWarehouseIngredientsAsync(Ingredients ingredients);
        public ValueTask RemoveWarehouseIngredientsAsync(Ingredients ingredient);
    }
}
