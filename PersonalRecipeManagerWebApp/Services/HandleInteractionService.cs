using Microsoft.EntityFrameworkCore;
using PersonalRecipeManagerWebApp.Brokers;
using PersonalRecipeManagerWebApp.Data;
using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public class HandleInteractionService(IDataBroker dataBroker) : IHandleInteractionService
    {
        private readonly IDataBroker _broker = dataBroker;

        // CHECK
        public async Task<bool> CheckIfKitchenHasEquipmentByIdAsync(Guid id)
        {
            var hasEquipment = await _broker.SelectKitchenEquipmentByIdAsync(id);
            return hasEquipment is not null;
        }
        public async Task<bool> CheckIfKitchenHasIngredientByIdAsync(Guid id)
        {
            var hasIngredient = await _broker.SelectKitchenIngredientByIdAsync(id);
            return hasIngredient is not null;
        }
        public async Task<bool> CheckIfRecipeHasIngredientByIdAsync(Guid id)
        {
            var hasIngredient = await _broker.SelectRecipeIngredientByIdAsync(id);
            return hasIngredient is not null;
        }
        public async Task<bool> CheckIfRecipeHasEquipmentByIdAsync(Guid id)
        {
            var hasEquipment = await _broker.SelectRecipeEquipmentByIdAsync(id);
            return hasEquipment is not null;
        }

        // ADD
        public async Task AddWarehouseEquipmentAsync(Equipment equipment)
        {
            await _broker.InsertWarehouseEquipmentAsync(equipment);
        }
        public async Task AddWarehouseIngredientsAsync(Ingredients ingredients)
        {

            await _broker.InsertWarehouseIngredientsAsync(ingredients);
        }
        public async Task AddKitchenEquipmentAsync(KitchenEquipment equipment)
        {

            await _broker.InsertKitchenEquipmentAsync(equipment);
        }
        public async Task AddKitchenIngredientsAsync(KitchenIngredients ingredients)
        {
            await _broker.InsertKitchenIngredientAsync(ingredients);
        }
        public async Task AddRecipeAsync(Recipe recipe)
        {
            await _broker.InsertRecipeAsync(recipe);
        }
        public async Task AddUserRecipeAsync(UserRecipes recipe)
        {
            await _broker.InsertUserRecipeAsync(recipe);
        }
        public async Task AddRecipeIngredientsAsync(RecipeIngredients ingredients)
        {
            await _broker.InsertRecipeIngredientsAsync(ingredients);
        }
        public async Task AddRecipeEquipmentAsync(RecipeEquipment equipment)
        {
            await _broker.InsertRecipeEquipmentAsync(equipment);
        }
        public async Task AddKitchenAsync(Kitchen kitchen)
        {
            await _broker.UpdateKitchenAsync(kitchen);
        }

        // RETRIEVE
        public async Task<List<Equipment>> RetrieveAllWarehouseEquipmentAsync()
        {
            return await _broker.SelectAllWarehouseEquipmentAsync();
        }
        public async Task<List<Ingredients>> RetrieveAllWarehouseIngredientsAsync()
        {
            return await _broker.SelectAllWarehouseIngredientsAsync();
        }
        public async Task<User> RetrieveUserInformationByIdAsync(Guid id)
        {
            return await _broker.SelectUserInformationByIdAsync(id);
        }
        public async Task<Kitchen> RetrieveKitchenByIdAsync(Guid id)
        {

            return await _broker.SelectKitchenByIdAsync(id);
        }
        public async Task<List<KitchenType>> RetrieveAllKitchenTypesAsync()
        {

            return await _broker.SelectAllKitchenTypesAsync();
        }
        public async Task<UserKitchen> RetrieveUserKitchenByIdAsync(Guid id)
        {

            return await _broker.SelectUserKitchenByIdAsync(id);
        }
        public async Task<List<EquipmentDto>> RetrieveKitchenEquipmentDtoByKitchenIdAsync(Guid id)
        {

            return await _broker.SelectKitchenEquipmentDtoByKitchenIdAsync(id);
        }
        public async Task<KitchenEquipment> RetrieveKitchenEquipmentByIdAsync(Guid id)
        {

            return await _broker.SelectKitchenEquipmentByIdAsync(id);

        }
        public async Task<List<KitchenIngredientsDto>> RetrieveKitchenIngredientsDtoByKitchenIdAsync(Guid id)
        {

            return await _broker.SelectKitchenIngredientsDtoByKitchenIdAsync(id);
        }
        public async Task<KitchenIngredients> RetrieveKitchenIngredientByIdAsync(Guid id)
        {

            return await _broker.SelectKitchenIngredientByIdAsync(id);

        }
        public async Task<List<Recipe>> RetrieveUserRecipesByIdAsync(Guid id)
        {

            return await _broker.SelectUserRecipesByIdAsync(id);
        }
        public async Task<List<RecipeIngredientsDto>> RetrieveRecipeIngredientsDtoByRecipeIdAsync(Guid id)
        {

            return await _broker.SelectRecipeIngredientsDtoByRecipeIdAsync(id);
        }
        public async Task<List<RecipeEquipmentDto>> RetrieveRecipeEquipmentDtoByRecipeIdAsync(Guid id)
        {

            return await _broker.SelectRecipeEquipmentDtoByRecipeIdAsync(id);
        }
        public async Task<Recipe> RetrieveRecipeByIdAsync(Guid id)
        {

            return await _broker.SelectRecipeByIdAsync(id);
        }
        public async Task<RecipeIngredients> RetrieveRecipeIngredientByIdAsync(Guid id)
        {
            return await _broker.SelectRecipeIngredientByIdAsync(id);
        }
        public async Task<RecipeEquipment> RetrieveRecipeEquipmentByIdAsync(Guid id)
        {
            return await _broker.SelectRecipeEquipmentByIdAsync(id);
        }
        public async Task<List<Recipe>> RetrieveRecipeCost(List<Recipe> recipes)
        {
            foreach (var recipe in recipes)
            {
                recipe.Cost = 0;
                List<RecipeIngredientsDto> recipeItems = await RetrieveRecipeIngredientsDtoByRecipeIdAsync(recipe.Id);
                foreach (var recipeItem in recipeItems)
                { 
                    recipe.Cost += await _broker.SelectIngredientCost(recipeItem.Id);
                }
            }
            return recipes;
        }
        public async Task<List<Kitchen>> RetrieveAllUserKitchens(Guid id)
        {
            return await _broker.SelectAllUserKitchensAsync(id);
        }

        // UPSERTS
        public async Task UpsertWarehouseEquipmentAsync(Equipment equipment)
        {
            var equipmentExists = await _broker.SelectWarehouseEquipmentByIdAsync(equipment.Id);
            if (equipmentExists is null)
            {
                await _broker.InsertWarehouseEquipmentAsync(equipment);
            }
            else
            {
                await _broker.UpdateWarehouseEquipmentAsync(equipment);
            }
        }
        public async Task UpsertWarehouseIngredientsAsync(Ingredients ingredients)
        {

            var ingredientExists = await _broker.SelectWarehouseIngredientByIdAsync(ingredients.Id);
            if (ingredientExists is null)
            {
                await _broker.InsertWarehouseIngredientsAsync(ingredients);
            }
            else
            {
                await _broker.UpdateWarehouseIngredientAsync(ingredients);
            }
        }
        public async Task UpsertKitchenEquipmentAsync(KitchenEquipment equipment)
        {
            var equipmentExists = await _broker.SelectKitchenEquipmentByIdAsync(equipment.KitchenId);
            
            if (equipmentExists is null)
            {
                await _broker.InsertKitchenEquipmentAsync(equipment);
            }
            else
            {
                await _broker.UpdateKitchenEquipmentAsync(equipment);
            }
        }
        public async Task UpsertKitchenIngredientsAsync(KitchenIngredients ingredient)
        {
            var ingredientExists = await _broker.SelectKitchenEquipmentByIdAsync(ingredient.KitchenId);

            if (ingredientExists is null)
            {
                await _broker.InsertKitchenIngredientAsync(ingredient);
            }
            else
            {
                await _broker.UpdateKitchenIngredientAsync(ingredient);
            }
        }
        public async Task UpsertRecipeAsync(Recipe recipe)
        {
            var recipeExists = await _broker.SelectRecipeByIdAsync(recipe.Id);

            if (recipeExists is null)
            {
                await _broker.InsertRecipeAsync(recipe);
            }
            else
            {
                await _broker.UpdateRecipeAsync(recipe);
            }
        }
        public async Task UpsertRecipeIngredientAsync(RecipeIngredients ingredient)
        {
            var ingredientExists = await _broker.SelectRecipeIngredientByIdAsync(ingredient.IngredientId);

            if (ingredientExists is null)
            {
                await _broker.InsertRecipeIngredientsAsync(ingredient);
            }
            else
            {
                await _broker.UpdateRecipeIngredientAsync(ingredient);
            }
        }
        public async Task UpsertRecipeEquipmentAsync(RecipeEquipment equipment)
        {
            var equipmentExists = await _broker.SelectRecipeIngredientByIdAsync(equipment.EquipmentId);

            if (equipmentExists is null)
            {
                await _broker.InsertRecipeEquipmentAsync(equipment);
            }
            else
            {
                await _broker.UpdateRecipeEquipmentAsync(equipment);
            }
        }
        public async Task UpsertKitchenAsync(Kitchen kitchen)
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
        public async Task UpsertUserKitchenAsync(UserKitchen kitchen)
        {

        }

        // UPDATES
        public async Task UpdateUserInformationAsync(User user)
        {
            await _broker.UpdateUserInformationAsync(user);
        }
   
        // REMOVES
        public async Task RemoveRecipeEquipmentRowAsync(RecipeEquipment equipment)
        {
            await _broker.DeleteRecipeEquipmentAsync(equipment);
        }
        public async Task RemoveRecipeIngredientRowAsync(RecipeIngredients ingredient)
        {
            await _broker.DeleteRecipeIngredientAsync(ingredient);
        }
    }

    
}
