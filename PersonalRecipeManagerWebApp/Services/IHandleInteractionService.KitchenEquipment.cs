using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask AddKitchenEquipmentAsync(KitchenEquipment equipment);
        public ValueTask<List<KitchenEquipmentViewModel>> RetrieveKitchenEquipmentDtoByKitchenIdAsync(Guid id);
        public ValueTask<KitchenEquipment> RetrieveKitchenEquipmentByIdAsync(Guid kitchenId, Guid equipmentId);
        public ValueTask UpsertKitchenEquipmentAsync(Guid kitchenId, KitchenEquipmentViewModel equipment);
        public ValueTask RemoveKitchenEquipmentAsync(Guid kitchenId, KitchenEquipmentViewModel equipment);
        public ValueTask<bool> CheckIfKitchenHasEquipmentByIdAsync(Guid kitchenId, Guid equipmentId);


    }
}
