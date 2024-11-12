using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models.Ingredients;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial interface IStorageBroker
    {
        ValueTask<RecipeIngredients> InsertRecipeIngredientsAsync(RecipeIngredients recipeIngredients);
        ValueTask<List<RecipeIngredients>> SelectAllRecipeIngredientsAsync();
        ValueTask<RecipeIngredients> SelectRecipeIngredientsByIdAsync(Guid recipeId, Guid ingredientId);
        ValueTask<RecipeIngredients> UpdateRecipeIngredientsAsync(RecipeIngredients recipeIngredients);
        ValueTask<RecipeIngredients> DeleteRecipeIngredientsAsync(RecipeIngredients recipeIngredients);
        ValueTask<List<RecipeEquipmentViewModel>> SelectRecipeEquipmentViewModelByRecipeIdAsync(Guid id);
    }
}
