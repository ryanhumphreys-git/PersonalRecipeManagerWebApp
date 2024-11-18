using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.Ingredients;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask<bool> AddKitchenIngredientsAsync(KitchenIngredients ingredients)
        {
            bool isSuccessful = false;
            KitchenIngredients insertKitchenIngredients = await _broker.InsertKitchenIngredientAsync(ingredients);
            if (insertKitchenIngredients is not null) isSuccessful = true;
            return isSuccessful;
        }
        public async ValueTask<List<KitchenIngredientsViewModel>> RetrieveKitchenIngredientsViewModelByKitchenIdAsync(Guid id)
        {
            return await _broker.SelectKitchenIngredientsViewModelByKitchenIdAsync(id);
        }
        public async ValueTask<KitchenIngredients> RetrieveKitchenIngredientByIdAsync(Guid kitchenId, Guid ingredientId)
        {
            return await _broker.SelectKitchenIngredientByIdAsync(kitchenId, ingredientId);

        }
        public async ValueTask<bool> UpsertKitchenIngredientsAsync(Guid kitchenId, KitchenIngredientsViewModel ingredient)
        {
            bool isSuccessful = false;
            KitchenIngredients ingredientExists = await _broker.SelectKitchenIngredientByIdAsync(kitchenId, ingredient.Id);

            if (ingredientExists is null)
            {
                ingredientExists = new(kitchenId, ingredient.Id, ingredient.Quantity);
                KitchenIngredients insertKitchenIngredient = await _broker.InsertKitchenIngredientAsync(ingredientExists);
                if (insertKitchenIngredient is not null) isSuccessful = true;
            }
            else
            {
                ingredientExists.IngredientId = ingredient.Id;
                ingredientExists.Quantity = ingredient.Quantity;

                KitchenIngredients updateKitchenIngredient = await _broker.UpdateKitchenIngredientAsync(ingredientExists);
                if (updateKitchenIngredient is not null) isSuccessful = true;
            }
            return isSuccessful;
        }
        public async ValueTask<bool> RemoveKitchenIngredientsAsync(Guid kitchenId, KitchenIngredientsViewModel ingredient)
        {
            bool isSuccessful = false;
            KitchenIngredients editIngredient = await _broker.SelectKitchenIngredientByIdAsync(kitchenId, ingredient.Id);
            KitchenIngredients deleteKitchenIngredients = await _broker.DeleteKitchenIngredientsAsync(editIngredient);
            if (deleteKitchenIngredients is not null) isSuccessful = true;
            return isSuccessful;
        }
        public async ValueTask<bool> CheckIfKitchenHasIngredientByIdAsync(Guid kitchenId, Guid ingredientId)
        {
            var hasIngredient = await _broker.SelectKitchenIngredientByIdAsync(kitchenId, ingredientId);
            return hasIngredient is not null;
        }
        public async ValueTask<bool> InsertKitchenIngredientsOrAddQuantityAsync(KitchenIngredients ingredient)
        {
            bool isSuccessful = false;
            KitchenIngredients ingredientsExists = await _broker.SelectKitchenIngredientByIdAsync(ingredient.KitchenId, ingredient.IngredientId);

            if (ingredientsExists is null)
            {
                ingredientsExists = new(ingredient.KitchenId, ingredient.IngredientId, ingredient.Quantity);
                KitchenIngredients insertKitchenIngredient = await _broker.InsertKitchenIngredientAsync(ingredientsExists);
                if (insertKitchenIngredient is not null) isSuccessful = true;
            }
            else
            {
                ingredientsExists.IngredientId = ingredient.IngredientId;
                ingredientsExists.Quantity += ingredient.Quantity;

                KitchenIngredients updateKitchenIngredient = await _broker.UpdateKitchenIngredientAsync(ingredientsExists);
                if (updateKitchenIngredient is not null) isSuccessful = true;
            }
            return isSuccessful;
        }

    }
}
