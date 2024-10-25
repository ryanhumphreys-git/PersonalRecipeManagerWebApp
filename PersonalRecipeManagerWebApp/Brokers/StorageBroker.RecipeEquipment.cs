using Microsoft.EntityFrameworkCore;
using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial class StorageBroker
    {
        public async ValueTask<RecipeEquipment> InsertRecipeEquipmentAsync(RecipeEquipment recipeEquipment) =>
            await InsertAsync(recipeEquipment);
        public async ValueTask<List<RecipeEquipment>> SelectAllRecipeEquipmentAsync() =>
            await SelectAllAsync<RecipeEquipment>();
        public async ValueTask<RecipeEquipment> SelectRecipeEquipmentByIdAsync(Guid[] ids) =>
            await SelectAsync<RecipeEquipment>(ids);
        public async ValueTask<RecipeEquipment> UpdateRecipeEquipmentAsync(RecipeEquipment recipeEquipment) =>
            await UpdateAsync(recipeEquipment);
        public async ValueTask<RecipeEquipment> DeleteRecipeEquipmentAsync(RecipeEquipment recipeEquipment) =>
            await DeleteAsync(recipeEquipment);
        public async ValueTask<List<RecipeEquipmentViewModel>> SelectRecipeEquipmentViewModelByRecipeIdAsync(Guid id)
        {

            return await db.Recipes
                .Where(r => r.Id == id)
                .SelectMany(r => r.RecipeEquipment)
                .Select(r => new RecipeEquipmentViewModel
                {
                    Id = r.Equipment!.Id,
                    Name = r.Equipment.Name,
                    Quantity = r.Quantity
                })
                .ToListAsync();
        }
    }
}
