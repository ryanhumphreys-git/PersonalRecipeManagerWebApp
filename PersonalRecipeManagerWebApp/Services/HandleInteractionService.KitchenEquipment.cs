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
        public async ValueTask<KitchenEquipment> RetrieveKitchenEquipmentByIdAsync(Guid id)
        {

            return await _broker.SelectKitchenEquipmentByIdAsync(id);

        }
        public async ValueTask UpsertKitchenEquipmentAsync(Guid kitchenId, KitchenEquipmentViewModel equipment)
        {
            KitchenEquipment equipmentExists = await _broker.SelectKitchenEquipmentByIdAsync(equipment.Id);

            if (equipmentExists is null)
            {
                equipmentExists = new(Guid.NewGuid(), kitchenId, equipment.Id, equipment.Quantity);
                await _broker.InsertKitchenEquipmentAsync(equipmentExists);
            }
            else
            {
                equipmentExists.EquipmentId = equipment.Id;
                equipmentExists.Quantity = equipment.Quantity;

                await _broker.UpdateKitchenEquipmentAsync(equipmentExists);
            }
        }
        public async ValueTask RemoveKitchenEquipmentAsync(KitchenEquipmentViewModel equipment)
        {
            KitchenEquipment editEquipment = await _broker.SelectKitchenEquipmentByIdAsync(equipment.Id);
            await _broker.DeleteKitchenEquipmentAsync(editEquipment);
        }
        public async ValueTask<bool> CheckIfKitchenHasEquipmentByIdAsync(Guid id)
        {
            var hasEquipment = await _broker.SelectKitchenEquipmentByIdAsync(id);
            return hasEquipment is not null;
        }

    }
}
