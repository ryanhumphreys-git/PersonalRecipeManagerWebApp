using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial class StorageBroker
    {
        public async ValueTask<Kitchen> InsertKitchenAsync(Kitchen kitchen) =>
            await InsertAsync(kitchen);
        public async ValueTask<List<Kitchen>> SelectAllKitchenAsync() =>
            await SelectAllAsync<Kitchen>();
        public async ValueTask<Kitchen> SelectKitchenByIdAsync(Guid kitchenId) =>
            await SelectAsync<Kitchen>(kitchenId);
        public async ValueTask<Kitchen> UpdateKitchenAsync(Kitchen kitchen) =>
            await UpdateAsync(kitchen);
        public async ValueTask<Kitchen> DeleteKitchenAsync(Kitchen kitchen) =>
            await DeleteAsync(kitchen);
    }
}
