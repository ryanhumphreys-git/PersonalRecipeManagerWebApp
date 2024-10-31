using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask AddRecipeEquipmentAsync(RecipeEquipment equipment)
        {
            await _broker.InsertRecipeEquipmentAsync(equipment);
        }
        public async ValueTask<List<RecipeEquipmentViewModel>> RetrieveRecipeEquipmentDtoByRecipeIdAsync(Guid id)
        {

            return await _broker.SelectRecipeEquipmentViewModelByRecipeIdAsync(id);
        }
        public async ValueTask<RecipeEquipment> RetrieveRecipeEquipmentByIdAsync(Guid recipeId, Guid equipmentId)
        {
            return await _broker.SelectRecipeEquipmentByIdAsync(recipeId, equipmentId);
        }
        public async ValueTask UpsertRecipeEquipmentAsync(Guid recipeId, RecipeEquipmentViewModel equipment)
        {
            RecipeEquipment equipmentExists = await _broker.SelectRecipeEquipmentByIdAsync(recipeId, equipment.Id);

            if (equipmentExists is null)
            {
                await _broker.InsertRecipeEquipmentAsync(equipmentExists);
            }
            else
            {
                await _broker.UpdateRecipeEquipmentAsync(equipmentExists);
            }
        }
        public async ValueTask RemoveRecipeEquipmentAsync(Guid recipeId, RecipeEquipmentViewModel equipment)
        {
            RecipeEquipment editEquipment = await _broker.SelectRecipeEquipmentByIdAsync(recipeId, equipment.Id);
            await _broker.DeleteRecipeEquipmentAsync(editEquipment);
        }
        public async ValueTask<bool> CheckIfRecipeHasEquipmentByIdAsync(Guid recipeId, Guid equipmentId)
        {
            var hasEquipment = await _broker.SelectRecipeEquipmentByIdAsync(recipeId, equipmentId);
            return hasEquipment is not null;
        }

    }
}
