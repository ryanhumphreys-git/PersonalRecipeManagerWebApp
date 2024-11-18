using PersonalRecipeManagerWebApp.Models.Ingredients;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask<bool> AddKitchenIngredientsAsync(KitchenIngredients ingredients);
        public ValueTask<List<KitchenIngredientsViewModel>> RetrieveKitchenIngredientsViewModelByKitchenIdAsync(Guid id);
        public ValueTask<KitchenIngredients> RetrieveKitchenIngredientByIdAsync(Guid kitchenId, Guid ingredientId);
        public ValueTask<bool> UpsertKitchenIngredientsAsync(Guid kitchenId, KitchenIngredientsViewModel ingredients);
        public ValueTask<bool> RemoveKitchenIngredientsAsync(Guid kitchenId, KitchenIngredientsViewModel ingredients);
        public ValueTask<bool> CheckIfKitchenHasIngredientByIdAsync(Guid kitchenId, Guid ingredientId);
        public ValueTask<bool> InsertKitchenIngredientsOrAddQuantityAsync(KitchenIngredients ingredient);

    }
}
