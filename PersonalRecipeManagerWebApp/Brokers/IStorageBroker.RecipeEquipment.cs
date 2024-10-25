using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial interface IStorageBroker
    {
        ValueTask<RecipeEquipment> InsertRecipeEquipmentAsync(RecipeEquipment recipeEquipment);
        ValueTask<List<RecipeEquipment>> SelectAllRecipeEquipmentAsync();
        ValueTask<RecipeEquipment> SelectRecipeEquipmentByIdAsync(Guid[] ids);
        ValueTask<RecipeEquipment> UpdateRecipeEquipmentAsync(RecipeEquipment recipeEquipment);
        ValueTask<RecipeEquipment> DeleteRecipeEquipmentAsync(RecipeEquipment recipeEquipment);
        ValueTask<List<RecipeIngredientsViewModel>> SelectRecipeIngredientsViewModelByRecipeIdAsync(Guid id);
    }
}
