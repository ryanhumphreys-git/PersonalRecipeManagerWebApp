using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial interface IStorageBroker
    {
        public ValueTask<List<KitchenType>> SelectAllKitchenTypesAsync();
        public ValueTask<KitchenType> SelectKitchenTypeByTypeIdAsync(Guid id);
    }
}
