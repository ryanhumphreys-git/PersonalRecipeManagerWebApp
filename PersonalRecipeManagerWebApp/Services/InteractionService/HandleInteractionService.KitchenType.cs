using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask<List<KitchenType>> RetrieveAllKitchenTypesAsync()
        {

            return await _broker.SelectAllKitchenTypesAsync();
        }
        public async ValueTask<KitchenType> RetrieveKitchenTypeByTypeIdAsync(Guid id)
        {
            return await _broker.SelectKitchenTypeByTypeIdAsync(id);
        }

    }
}
