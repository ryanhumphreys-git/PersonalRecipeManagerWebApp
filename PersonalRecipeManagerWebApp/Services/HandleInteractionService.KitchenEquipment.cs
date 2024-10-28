using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask AddKitchenEquipmentAsync(KitchenEquipment equipment)
        {

            await _broker.InsertKitchenEquipmentAsync(equipment);
        }
        public async ValueTask<List<KitchenEquipmentViewModel>> RetrieveKitchenEquipmentDtoByKitchenIdAsync(Guid id)
        {

            return await _broker.SelectKitchenEquipmentViewModelByKitchenIdAsync(id);
        }
        public async ValueTask<KitchenEquipment> RetrieveKitchenEquipmentByIdAsync(Guid kitchenId, Guid equipmentId)
        {
            return await _broker.SelectKitchenEquipmentByIdAsync(kitchenId, equipmentId);

        }
        public async ValueTask UpsertKitchenEquipmentAsync(Guid kitchenId, KitchenEquipmentViewModel equipment)
        {
            KitchenEquipment equipmentExists = await _broker.SelectKitchenEquipmentByIdAsync(kitchenId, equipment.Id);

            if (equipmentExists is null)
            {
                equipmentExists = new(kitchenId, equipment.Id, equipment.Quantity);
                await _broker.InsertKitchenEquipmentAsync(equipmentExists);
            }
            else
            {
                equipmentExists.EquipmentId = equipment.Id;
                equipmentExists.Quantity = equipment.Quantity;

                await _broker.UpdateKitchenEquipmentAsync(equipmentExists);
            }
        }
        public async ValueTask RemoveKitchenEquipmentAsync(Guid kitchenId, KitchenEquipmentViewModel equipment)
        {
            KitchenEquipment editEquipment = await _broker.SelectKitchenEquipmentByIdAsync(kitchenId, equipment.Id);
            await _broker.DeleteKitchenEquipmentAsync(editEquipment);
        }
        public async ValueTask<bool> CheckIfKitchenHasEquipmentByIdAsync(Guid kitchenId, Guid equipmentId)
        {
            var hasEquipment = await _broker.SelectKitchenEquipmentByIdAsync(kitchenId, equipmentId);
            return hasEquipment is not null;
        }

    }
}
