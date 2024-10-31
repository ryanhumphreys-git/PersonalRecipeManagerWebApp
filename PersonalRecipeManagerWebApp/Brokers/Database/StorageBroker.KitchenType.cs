using Microsoft.EntityFrameworkCore;
using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial class StorageBroker
    {
        public async ValueTask<List<KitchenType>> SelectAllKitchenTypesAsync() =>
            await SelectAllAsync<KitchenType>();
        public async ValueTask<KitchenType> SelectKitchenTypeByTypeIdAsync(Guid typeId) =>
            await SelectAsync<KitchenType>(typeId);
    }
}
