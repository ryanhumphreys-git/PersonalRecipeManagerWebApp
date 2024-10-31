using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial interface IStorageBroker
    {
        ValueTask<Equipment> InsertWarehouseEquipmentAsync(Equipment equipment);
        ValueTask<List<Equipment>> SelectAllWarehouseEquipmentAsync();
        ValueTask<Equipment> SelectWarehouseEquipmentByIdAsync(Guid equipmentId);
        ValueTask<Equipment> UpdateWarehouseEquipmentAsync(Equipment equipment);
        ValueTask<Equipment> DeleteWarehouseEquipmentAsync(Equipment equipment);
    }
}
