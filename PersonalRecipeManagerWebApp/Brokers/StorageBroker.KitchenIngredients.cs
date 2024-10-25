using Microsoft.EntityFrameworkCore;
using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial class StorageBroker
    {
        public async ValueTask<KitchenIngredients> InsertKitchenIngredientAsync(KitchenIngredients ingredient) =>
            await InsertAsync(ingredient);
        public async ValueTask<List<KitchenIngredients>> SelectAllKitchenIngredientsAsync() =>
            await SelectAllAsync<KitchenIngredients>();
        public async ValueTask<KitchenIngredients> SelectKitchenIngredientByIdAsync(Guid ingredientId) =>
            await SelectAsync<KitchenIngredients>(ingredientId);
        public async ValueTask<KitchenIngredients> UpdateKitchenIngredientAsync(KitchenIngredients ingredient) =>
            await UpdateAsync(ingredient);
        public async ValueTask<KitchenIngredients> DeleteKitchenIngredientsAsync(KitchenIngredients ingredient) =>
            await DeleteAsync(ingredient);
        public async ValueTask<List<KitchenIngredientsViewModel>> SelectKitchenIngredientsViewModelByKitchenIdAsync(Guid id)
        {

            return await db.Kitchen
                .Where(k => k.Id == id)
                .SelectMany(k => k.KitchenIngredients)
                .Select(k => new KitchenIngredientsViewModel
                {
                    Id = k.Ingredient.Id,
                    Name = k.Ingredient.Name,
                    Quantity = k.Quantity,
                    UnitOfMeasurement = k.Ingredient.UnitOfMeasurement
                })
                .ToListAsync();
        }
    }
}
