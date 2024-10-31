using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask AddKitchenAsync(Kitchen kitchen)
        {
            await _broker.UpdateKitchenAsync(kitchen);
        }
        public async ValueTask UpsertKitchenAsync(Kitchen kitchen)
        {
            var kitchenExists = await _broker.SelectKitchenByIdAsync(kitchen.Id);

            if (kitchenExists is null)
            {
                await _broker.UpdateKitchenAsync(kitchen);
            }
            else
            {
                await _broker.UpdateKitchenAsync(kitchen);
            }
        }
        public async ValueTask RemoveKitchenAsync(Kitchen kitchen)
        {
            await _broker.DeleteKitchenAsync(kitchen);
        }
        public async ValueTask<Kitchen> RetrieveKitchenByIdAsync(Guid id)
        {

            return await _broker.SelectKitchenByIdAsync(id);
        }

    }
}
