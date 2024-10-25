using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial interface IStorageBroker
    {
        ValueTask<Kitchen> InsertKitchenAsync(Kitchen kitchen);
        ValueTask<List<Kitchen>> SelectAllKitchenAsync();
        ValueTask<Kitchen> SelectKitchenByIdAsync(Guid kitchenId);
        ValueTask<Kitchen> UpdateKitchenAsync(Kitchen kitchen);
        ValueTask<Kitchen> DeleteKitchenAsync(Kitchen kitchen);
    }
}
