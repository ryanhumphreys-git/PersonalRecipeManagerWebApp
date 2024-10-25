using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial interface IStorageBroker
    {
        ValueTask<Ingredients> InsertWarehouseIngredientsAsync(Ingredients ingredient);
        ValueTask<List<Ingredients>> SelectAllWarehouseIngredientsAsync();
        ValueTask<Ingredients> SelectWarehouseIngredientsByIdAsync(Guid ingredientId);
        ValueTask<Ingredients> UpdateWarehouseIngredientsAsync(Ingredients ingredient);
        ValueTask<Ingredients> DeleteWarehouseIngredientsAsync(Ingredients ingredient);
    }
}
