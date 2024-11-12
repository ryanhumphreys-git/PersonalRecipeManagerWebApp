using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask<bool> AddKitchenAsync(Kitchen kitchen);
        public ValueTask<Kitchen> RetrieveKitchenByIdAsync(Guid id);
        public ValueTask<bool> UpsertKitchenAsync(Kitchen kitchen);
        public ValueTask<bool> RemoveKitchenAsync(Kitchen kitchen);

    }
}
