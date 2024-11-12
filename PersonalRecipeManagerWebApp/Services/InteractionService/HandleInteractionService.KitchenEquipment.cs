using PersonalRecipeManagerWebApp.Models.Equipment;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask<bool> AddKitchenEquipmentAsync(KitchenEquipment equipment)
        {
            bool isSuccessful = false;
            KitchenEquipment insertKitchenEquipemnt = await _broker.InsertKitchenEquipmentAsync(equipment);
            if (insertKitchenEquipemnt is not null) isSuccessful = true;
            return isSuccessful;
        }
        public async ValueTask<List<KitchenEquipmentViewModel>> RetrieveKitchenEquipmentDtoByKitchenIdAsync(Guid id)
        {

            return await _broker.SelectKitchenEquipmentViewModelByKitchenIdAsync(id);
        }
        public async ValueTask<KitchenEquipment> RetrieveKitchenEquipmentByIdAsync(Guid kitchenId, Guid equipmentId)
        {
            return await _broker.SelectKitchenEquipmentByIdAsync(kitchenId, equipmentId);

        }
        public async ValueTask<bool> UpsertKitchenEquipmentAsync(Guid kitchenId, KitchenEquipmentViewModel equipment)
        {
            KitchenEquipment equipmentExists = await _broker.SelectKitchenEquipmentByIdAsync(kitchenId, equipment.Id);
            bool isSuccessful = false;

            if (equipmentExists is null)
            {
                equipmentExists = new(kitchenId, equipment.Id, equipment.Quantity);
                KitchenEquipment insertedKitchenEquipment = await _broker.InsertKitchenEquipmentAsync(equipmentExists);
                if (insertedKitchenEquipment is not null) isSuccessful = true;
            }
            else
            {
                equipmentExists.EquipmentId = equipment.Id;
                equipmentExists.Quantity = equipment.Quantity;

                KitchenEquipment updatedKitchenEquipment = await _broker.UpdateKitchenEquipmentAsync(equipmentExists);
                if (updatedKitchenEquipment is not null) isSuccessful = true;
            }
            return isSuccessful;
        }
        public async ValueTask<bool> RemoveKitchenEquipmentAsync(Guid kitchenId, KitchenEquipmentViewModel equipment)
        {
            bool isSuccessful = false;
            KitchenEquipment editEquipment = await _broker.SelectKitchenEquipmentByIdAsync(kitchenId, equipment.Id);
            KitchenEquipment deleteKitchenEquipment = await _broker.DeleteKitchenEquipmentAsync(editEquipment);
            if (deleteKitchenEquipment is not null) isSuccessful = true;
            return isSuccessful;
        }
        public async ValueTask<bool> CheckIfKitchenHasEquipmentByIdAsync(Guid kitchenId, Guid equipmentId)
        {
            var hasEquipment = await _broker.SelectKitchenEquipmentByIdAsync(kitchenId, equipmentId);
            return hasEquipment is not null;
        }

    }
}
