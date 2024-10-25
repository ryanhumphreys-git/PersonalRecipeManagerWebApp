using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask AddWarehouseEquipmentAsync(Equipment equipment);
        public ValueTask<List<Equipment>> RetrieveAllWarehouseEquipmentAsync();
        public ValueTask<Equipment> RetrieveWarehouseEquipmentByIdAsync(Guid id);
        public ValueTask UpsertWarehouseEquipmentAsync(Equipment equipment);
        public ValueTask RemoveWarehouseEquipmentAsync(Equipment equipment);

    }
}
