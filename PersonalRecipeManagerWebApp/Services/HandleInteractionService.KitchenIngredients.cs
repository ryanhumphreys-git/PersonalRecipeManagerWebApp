using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask AddKitchenIngredientsAsync(KitchenIngredients ingredients)
        {
            await _broker.InsertKitchenIngredientAsync(ingredients);
        }
        public async ValueTask<List<KitchenIngredientsViewModel>> RetrieveKitchenIngredientsDtoByKitchenIdAsync(Guid id)
        {
            return await _broker.SelectKitchenIngredientsViewModelByKitchenIdAsync(id);
        }
        public async ValueTask<KitchenIngredients> RetrieveKitchenIngredientByIdAsync(Guid kitchenId, Guid ingredientId)
        {
            return await _broker.SelectKitchenIngredientByIdAsync(kitchenId, ingredientId);

        }
        public async ValueTask UpsertKitchenIngredientsAsync(Guid kitchenId, KitchenIngredientsViewModel ingredient)
        {
            KitchenIngredients ingredientExists = await _broker.SelectKitchenIngredientByIdAsync(kitchenId, ingredient.Id);

            if (ingredientExists is null)
            {
                ingredientExists = new(kitchenId, ingredient.Id, ingredient.Quantity);
                await _broker.InsertKitchenIngredientAsync(ingredientExists);
            }
            else
            {
                ingredientExists.IngredientId = ingredient.Id;
                ingredientExists.Quantity = ingredient.Quantity;

                await _broker.UpdateKitchenIngredientAsync(ingredientExists);
            }
        }
        public async ValueTask RemoveKitchenIngredientsAsync(Guid kitchenId, KitchenIngredientsViewModel ingredient)
        {
            KitchenIngredients editIngredient = await _broker.SelectKitchenIngredientByIdAsync(kitchenId, ingredient.Id);
            await _broker.DeleteKitchenIngredientsAsync(editIngredient);
        }
        public async ValueTask<bool> CheckIfKitchenHasIngredientByIdAsync(Guid kitchenId, Guid ingredientId)
        {
            var hasIngredient = await _broker.SelectKitchenIngredientByIdAsync(kitchenId, ingredientId);
            return hasIngredient is not null;
        }

    }
}
