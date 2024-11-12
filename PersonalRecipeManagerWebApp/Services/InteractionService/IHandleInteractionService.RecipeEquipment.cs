using PersonalRecipeManagerWebApp.Models.Equipment;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask<bool> AddRecipeEquipmentAsync(RecipeEquipment equipment);
        public ValueTask<List<RecipeEquipmentViewModel>> RetrieveRecipeEquipmentDtoByRecipeIdAsync(Guid id);
        public ValueTask<RecipeEquipment> RetrieveRecipeEquipmentByIdAsync(Guid recipeId, Guid equipmentId);
        public ValueTask<bool> UpsertRecipeEquipmentAsync(Guid recipeId, RecipeEquipmentViewModel equipment);
        public ValueTask<bool> RemoveRecipeEquipmentAsync(Guid recipeId, RecipeEquipmentViewModel equipment);
        public ValueTask<bool> CheckIfRecipeHasEquipmentByIdAsync(Guid recipeId, Guid equipmentId);

    }
}
