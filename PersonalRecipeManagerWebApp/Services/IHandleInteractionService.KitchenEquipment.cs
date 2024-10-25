using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask AddKitchenEquipmentAsync(KitchenEquipment equipment);
        public ValueTask<List<KitchenEquipmentViewModel>> RetrieveKitchenEquipmentDtoByKitchenIdAsync(Guid id);
        public ValueTask<KitchenEquipment> RetrieveKitchenEquipmentByIdAsync(Guid id);
        public ValueTask UpsertKitchenEquipmentAsync(Guid kitchenId, KitchenEquipmentViewModel equipment);
        public ValueTask RemoveKitchenEquipmentAsync(KitchenEquipmentViewModel equipment);
        public ValueTask<bool> CheckIfKitchenHasEquipmentByIdAsync(Guid id);


    }
}
