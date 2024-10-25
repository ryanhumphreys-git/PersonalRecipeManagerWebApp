using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask<List<KitchenType>> RetrieveAllKitchenTypesAsync();
        public ValueTask<KitchenType> RetrieveKitchenTypeByTypeIdAsync(Guid id);

    }
}
