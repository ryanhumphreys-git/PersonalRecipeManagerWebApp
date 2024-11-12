using Microsoft.EntityFrameworkCore;
using PersonalRecipeManagerWebApp.Models.Ingredients;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial class StorageBroker
    {
        public async ValueTask<Ingredients> InsertWarehouseIngredientsAsync(Ingredients ingredient) =>
            await InsertAsync(ingredient);
        public async ValueTask<List<Ingredients>> SelectAllWarehouseIngredientsAsync() =>
            await SelectAllAsync<Ingredients>();
        public async ValueTask<Ingredients> SelectWarehouseIngredientsByIdAsync(Guid ingredientId) =>
            await SelectAsync<Ingredients>(ingredientId);
        public async ValueTask<Ingredients> UpdateWarehouseIngredientsAsync(Ingredients ingredient) =>
            await UpdateAsync(ingredient);
        public async ValueTask<Ingredients> DeleteWarehouseIngredientsAsync(Ingredients ingredient) =>
            await DeleteAsync(ingredient);
        public async ValueTask<Ingredients> SelectWarehouseIngredientsByNameAsync(string name)
        {
            return await db.Ingredients.Where(i => i.Name == name).FirstOrDefaultAsync();
        }
    }
}
