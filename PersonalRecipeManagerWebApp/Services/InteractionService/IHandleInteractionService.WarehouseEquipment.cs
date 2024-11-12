using PersonalRecipeManagerWebApp.Models.Equipment;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask<bool> AddWarehouseEquipmentAsync(Equipment equipment);
        public ValueTask<List<Equipment>> RetrieveAllWarehouseEquipmentAsync();
        public ValueTask<Equipment> RetrieveWarehouseEquipmentByIdAsync(Guid id);
        public ValueTask<bool> UpsertWarehouseEquipmentAsync(Equipment equipment);
        public ValueTask<bool> RemoveWarehouseEquipmentAsync(Equipment equipment);

    }
}
