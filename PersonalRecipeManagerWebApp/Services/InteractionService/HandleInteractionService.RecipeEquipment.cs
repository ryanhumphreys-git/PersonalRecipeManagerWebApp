using PersonalRecipeManagerWebApp.Models.Equipment;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask<bool> AddRecipeEquipmentAsync(RecipeEquipment equipment)
        {
            bool isSuccessful = false;
            RecipeEquipment insertRecipeEquipmentSuccess = await _broker.InsertRecipeEquipmentAsync(equipment);
            if (insertRecipeEquipmentSuccess is not null) isSuccessful = true;
            return isSuccessful;
        }
        public async ValueTask<List<RecipeEquipmentViewModel>> RetrieveRecipeEquipmentDtoByRecipeIdAsync(Guid id)
        {

            return await _broker.SelectRecipeEquipmentViewModelByRecipeIdAsync(id);
        }
        public async ValueTask<RecipeEquipment> RetrieveRecipeEquipmentByIdAsync(Guid recipeId, Guid equipmentId)
        {
            return await _broker.SelectRecipeEquipmentByIdAsync(recipeId, equipmentId);
        }
        public async ValueTask<bool> UpsertRecipeEquipmentAsync(Guid recipeId, RecipeEquipmentViewModel equipment)
        {
            bool isSuccessful = false;
            RecipeEquipment equipmentExists = await _broker.SelectRecipeEquipmentByIdAsync(recipeId, equipment.Id);

            if (equipmentExists is null)
            {
                equipmentExists = new(recipeId, equipment.Id, equipment.Quantity);
                RecipeEquipment insertRecipeEquipment = await _broker.InsertRecipeEquipmentAsync(equipmentExists);
                if (insertRecipeEquipment is not null) isSuccessful = true;
            }
            else
            {
                equipmentExists.EquipmentId = equipment.Id;
                equipmentExists.Quantity = equipment.Quantity;
                RecipeEquipment updateRecipeEquipemnt = await _broker.UpdateRecipeEquipmentAsync(equipmentExists);
                if (updateRecipeEquipemnt is not null) isSuccessful = true;
            }
            return isSuccessful;
        }
        public async ValueTask<bool> RemoveRecipeEquipmentAsync(Guid recipeId, RecipeEquipmentViewModel equipment)
        {
            bool isSuccessful = false;
            RecipeEquipment editEquipment = await _broker.SelectRecipeEquipmentByIdAsync(recipeId, equipment.Id);
            RecipeEquipment deleteRecipeEquipment = await _broker.DeleteRecipeEquipmentAsync(editEquipment);
            if (deleteRecipeEquipment is not null) isSuccessful = true;
            return isSuccessful;
        }
        public async ValueTask<bool> CheckIfRecipeHasEquipmentByIdAsync(Guid recipeId, Guid equipmentId)
        {
            var hasEquipment = await _broker.SelectRecipeEquipmentByIdAsync(recipeId, equipmentId);
            return hasEquipment is not null;
        }

    }
}
