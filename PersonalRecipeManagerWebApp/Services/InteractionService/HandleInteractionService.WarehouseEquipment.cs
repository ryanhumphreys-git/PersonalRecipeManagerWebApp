using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask AddWarehouseEquipmentAsync(Equipment equipment)
        {
            await _broker.InsertWarehouseEquipmentAsync(equipment);
        }
        public async ValueTask<List<Equipment>> RetrieveAllWarehouseEquipmentAsync()
        {
            return await _broker.SelectAllWarehouseEquipmentAsync();
        }
        public async ValueTask<Equipment> RetrieveWarehouseEquipmentByIdAsync(Guid id)
        {
            return await _broker.SelectWarehouseEquipmentByIdAsync(id);
        }
        public async ValueTask UpsertWarehouseEquipmentAsync(Equipment equipment)
        {
            var equipmentExists = await _broker.SelectWarehouseEquipmentByIdAsync(equipment.Id);
            if (equipmentExists is null)
            {
                await _broker.InsertWarehouseEquipmentAsync(equipment);
            }
            else
            {
                await _broker.UpdateWarehouseEquipmentAsync(equipment);
            }
        }
        public async ValueTask RemoveWarehouseEquipmentAsync(Equipment equipment)
        {
            await _broker.DeleteWarehouseEquipmentAsync(equipment);
        }

    }
}
