using Microsoft.EntityFrameworkCore;
using PersonalRecipeManagerWebApp.Models.Equipment;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial class StorageBroker
    {
        public async ValueTask<KitchenEquipment> InsertKitchenEquipmentAsync(KitchenEquipment equipment) =>
            await InsertAsync(equipment);
        public async ValueTask<List<KitchenEquipment>> SelectAllKitchenEquipmentAsync() =>
            await SelectAllAsync<KitchenEquipment>();
        public async ValueTask<KitchenEquipment> SelectKitchenEquipmentByIdAsync(Guid kitchenId, Guid EquipmentId) =>
            await SelectAsync<KitchenEquipment>(kitchenId, EquipmentId);
        public async ValueTask<KitchenEquipment> UpdateKitchenEquipmentAsync(KitchenEquipment equipment) =>
            await UpdateAsync(equipment);
        public async ValueTask<KitchenEquipment> DeleteKitchenEquipmentAsync(KitchenEquipment equipment) =>
            await DeleteAsync(equipment);
        public async ValueTask<List<KitchenEquipmentViewModel>> SelectKitchenEquipmentViewModelByKitchenIdAsync(Guid id)
        {

            return await db.Kitchen
                    .Where(k => k.Id == id)
                    .SelectMany(k => k.KitchenEquipment)
                    .Select(k => new KitchenEquipmentViewModel
                    {
                        Id = k.Equipment!.Id,
                        Name = k.Equipment.Name,
                        Quantity = k.Quantity
                    })
                    .ToListAsync();
        }
    }
}
