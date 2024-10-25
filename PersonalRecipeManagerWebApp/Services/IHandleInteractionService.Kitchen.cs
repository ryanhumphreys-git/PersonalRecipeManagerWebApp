using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask AddKitchenAsync(Kitchen kitchen);
        public ValueTask<Kitchen> RetrieveKitchenByIdAsync(Guid id);
        public ValueTask UpsertKitchenAsync(Kitchen kitchen);
        public ValueTask RemoveKitchenAsync(Kitchen kitchen);

    }
}
