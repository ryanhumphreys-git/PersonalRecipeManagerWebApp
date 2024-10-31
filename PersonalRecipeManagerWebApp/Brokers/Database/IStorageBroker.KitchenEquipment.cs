using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial interface IStorageBroker
    {
        ValueTask<KitchenEquipment> InsertKitchenEquipmentAsync(KitchenEquipment equipment);
        ValueTask<List<KitchenEquipment>> SelectAllKitchenEquipmentAsync();
        ValueTask<KitchenEquipment> SelectKitchenEquipmentByIdAsync(Guid kitchenId, Guid equipmentId);
        ValueTask<KitchenEquipment> UpdateKitchenEquipmentAsync(KitchenEquipment equipment);
        ValueTask<KitchenEquipment> DeleteKitchenEquipmentAsync(KitchenEquipment equipment);
        ValueTask<List<KitchenEquipmentViewModel>> SelectKitchenEquipmentViewModelByKitchenIdAsync(Guid id);
    }
}
