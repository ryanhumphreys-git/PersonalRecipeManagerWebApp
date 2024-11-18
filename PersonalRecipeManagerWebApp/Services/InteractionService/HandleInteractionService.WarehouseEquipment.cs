using PersonalRecipeManagerWebApp.Models.Equipment;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask<bool> AddWarehouseEquipmentAsync(Equipment equipment)
        {
            bool isSuccessful = false;
            Equipment insertWarehouseEquipment = await _broker.InsertWarehouseEquipmentAsync(equipment);
            if (insertWarehouseEquipment is not null) isSuccessful = true;
            return isSuccessful;    
        }
        public async ValueTask<List<Equipment>> RetrieveAllWarehouseEquipmentAsync()
        {
            return await _broker.SelectAllWarehouseEquipmentAsync();
        }
        public async ValueTask<Equipment> RetrieveWarehouseEquipmentByIdAsync(Guid id)
        {
            return await _broker.SelectWarehouseEquipmentByIdAsync(id);
        }
        public async ValueTask<bool> UpsertWarehouseEquipmentAsync(Equipment equipment)
        {
            bool isSuccessful = false;
            var equipmentExists = await _broker.SelectWarehouseEquipmentByIdAsync(equipment.Id);
            if (equipmentExists is null)
            {
                Equipment insertWarehouseEquipment = await _broker.InsertWarehouseEquipmentAsync(equipment);
                if (insertWarehouseEquipment is not null) isSuccessful = true;
            }
            else
            {
                Equipment updateWarehouseEquipment = await _broker.UpdateWarehouseEquipmentAsync(equipment);
                if (updateWarehouseEquipment is not null) isSuccessful = true;
            }
            return isSuccessful;
        }
        public async ValueTask<bool> RemoveWarehouseEquipmentAsync(Equipment equipment)
        {
            bool isSuccessful = false;
            try
            {
                Equipment deleteWarehouseEquipment = await _broker.DeleteWarehouseEquipmentAsync(equipment);
                if (deleteWarehouseEquipment is not null) isSuccessful = true;
            }
            catch
            {
                isSuccessful = false;
            }
            return isSuccessful;
        }

    }
}
