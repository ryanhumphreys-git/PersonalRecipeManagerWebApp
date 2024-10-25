using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial class StorageBroker
    {
        public async ValueTask<Equipment> InsertWarehouseEquipmentAsync(Equipment equipment) =>
            await InsertAsync(equipment);
        
        public async ValueTask<List<Equipment>> SelectAllWarehouseEquipmentAsync() =>
            await SelectAllAsync<Equipment>();
        
        public async ValueTask<Equipment> SelectWarehouseEquipmentByIdAsync(Guid equipmentId) =>
            await SelectAsync<Equipment>(equipmentId);
        public async ValueTask<Equipment> UpdateWarehouseEquipmentAsync(Equipment equipment) =>
            await UpdateAsync(equipment);
        public async ValueTask<Equipment> DeleteWarehouseEquipmentAsync(Equipment equipment) =>
            await DeleteAsync(equipment);
    }
}
