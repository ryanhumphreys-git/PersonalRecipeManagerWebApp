using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask <bool> AddKitchenAsync(Kitchen kitchen)
        {
            bool isSuccessful = false;
            Kitchen insertKitchen = await _broker.InsertKitchenAsync(kitchen);
            if (insertKitchen is not null) isSuccessful = true;
            return isSuccessful;
        }
        public async ValueTask<bool> UpsertKitchenAsync(Kitchen kitchen)
        {
            bool isSuccessful = false;  
            var kitchenExists = await _broker.SelectKitchenByIdAsync(kitchen.Id);

            if (kitchenExists is null)
            {
                Kitchen insertKitchen = await _broker.InsertKitchenAsync(kitchen);
                if (insertKitchen is not null) isSuccessful = true;
            }
            else
            {
                Kitchen updateKitchen = await _broker.UpdateKitchenAsync(kitchen);
                if (updateKitchen is not null) isSuccessful = true;
            }
            return isSuccessful;
        }
        public async ValueTask<bool> RemoveKitchenAsync(Kitchen kitchen)
        {
            bool isSuccessful = false;
            Kitchen deleteKitchen = await _broker.DeleteKitchenAsync(kitchen);
            if (deleteKitchen is not null) isSuccessful = true;
            return isSuccessful;
        }
        public async ValueTask<Kitchen> RetrieveKitchenByIdAsync(Guid id)
        {

            return await _broker.SelectKitchenByIdAsync(id);
        }

    }
}
