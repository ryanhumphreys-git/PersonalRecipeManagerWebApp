using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models.Ingredients;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial interface IStorageBroker
    {
        ValueTask<RecipeEquipment> InsertRecipeEquipmentAsync(RecipeEquipment recipeEquipment);
        ValueTask<List<RecipeEquipment>> SelectAllRecipeEquipmentAsync();
        ValueTask<RecipeEquipment> SelectRecipeEquipmentByIdAsync(Guid recipeId, Guid equipmentId);
        ValueTask<RecipeEquipment> UpdateRecipeEquipmentAsync(RecipeEquipment recipeEquipment);
        ValueTask<RecipeEquipment> DeleteRecipeEquipmentAsync(RecipeEquipment recipeEquipment);
        ValueTask<List<RecipeIngredientsViewModel>> SelectRecipeIngredientsViewModelByRecipeIdAsync(Guid id);
    }
}
