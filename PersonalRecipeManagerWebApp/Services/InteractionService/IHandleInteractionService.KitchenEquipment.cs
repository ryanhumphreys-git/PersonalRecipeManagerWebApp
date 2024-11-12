using PersonalRecipeManagerWebApp.Models.Equipment;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask<bool> AddKitchenEquipmentAsync(KitchenEquipment equipment);
        public ValueTask<List<KitchenEquipmentViewModel>> RetrieveKitchenEquipmentDtoByKitchenIdAsync(Guid id);
        public ValueTask<KitchenEquipment> RetrieveKitchenEquipmentByIdAsync(Guid kitchenId, Guid equipmentId);
        public ValueTask<bool> UpsertKitchenEquipmentAsync(Guid kitchenId, KitchenEquipmentViewModel equipment);
        public ValueTask<bool> RemoveKitchenEquipmentAsync(Guid kitchenId, KitchenEquipmentViewModel equipment);
        public ValueTask<bool> CheckIfKitchenHasEquipmentByIdAsync(Guid kitchenId, Guid equipmentId);


    }
}
