using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public interface IHandleInteractionService
    {
        // CHECKS
        public Task<bool> CheckIfKitchenHasEquipmentByIdAsync(Guid id);
        public Task<bool> CheckIfKitchenHasIngredientByIdAsync(Guid id);
        public Task<bool> CheckIfRecipeHasIngredientByIdAsync(Guid id);
        public Task<bool> CheckIfRecipeHasEquipmentByIdAsync(Guid id);

        // INSERTS
        public Task AddWarehouseEquipmentAsync(Equipment equipment);
        public Task AddWarehouseIngredientsAsync(Ingredients ingredients);
        public Task AddKitchenEquipmentAsync(KitchenEquipment equipment);
        public Task AddKitchenIngredientsAsync(KitchenIngredients ingredients);
        public Task AddRecipeAsync(Recipe recipe);
        public Task AddUserRecipeAsync(UserRecipes recipe);
        public Task AddRecipeIngredientsAsync(RecipeIngredients ingredients);
        public Task AddRecipeEquipmentAsync(RecipeEquipment equipment);
        public Task AddKitchenAsync(Kitchen kitchen);


        // GETS
        public Task<List<Equipment>> RetrieveAllWarehouseEquipmentAsync();
        public Task<List<Ingredients>> RetrieveAllWarehouseIngredientsAsync();
        public Task<User> RetrieveUserInformationByIdAsync(Guid id);
        public Task<Kitchen> RetrieveKitchenByIdAsync(Guid id);
        public Task<List<KitchenType>> RetrieveAllKitchenTypesAsync();
        public Task<UserKitchen> RetrieveUserKitchenByIdAsync(Guid id);
        public Task<List<EquipmentDto>> RetrieveKitchenEquipmentDtoByKitchenIdAsync(Guid id);
        public Task<KitchenEquipment> RetrieveKitchenEquipmentByIdAsync(Guid id);
        public Task<List<KitchenIngredientsDto>> RetrieveKitchenIngredientsDtoByKitchenIdAsync(Guid id);
        public Task<KitchenIngredients> RetrieveKitchenIngredientByIdAsync(Guid id);
        public Task<List<Recipe>> RetrieveUserRecipesByIdAsync(Guid id);
        public Task<List<RecipeIngredientsDto>> RetrieveRecipeIngredientsDtoByRecipeIdAsync(Guid id);
        public Task<List<RecipeEquipmentDto>> RetrieveRecipeEquipmentDtoByRecipeIdAsync(Guid id);
        public Task<Recipe> RetrieveRecipeByIdAsync(Guid id);
        public Task<RecipeIngredients> RetrieveRecipeIngredientByIdAsync(Guid id);
        public Task<RecipeEquipment> RetrieveRecipeEquipmentByIdAsync(Guid id);
        public Task<List<Recipe>> RetrieveRecipeCost(List<Recipe> recipes);
        public Task<List<Kitchen>> RetrieveAllUserKitchens(Guid id);

        // UPSERTS
        public Task UpsertWarehouseEquipmentAsync(Equipment equipment);
        public Task UpsertWarehouseIngredientsAsync(Ingredients ingredients);
        public Task UpsertKitchenEquipmentAsync(KitchenEquipment equipment);
        public Task UpsertKitchenIngredientsAsync(KitchenIngredients ingredients);
        public Task UpsertRecipeAsync(Recipe recipe);
        public Task UpsertRecipeIngredientAsync(RecipeIngredients ingredient);
        public Task UpsertRecipeEquipmentAsync(RecipeEquipment equipment);
        public Task UpsertKitchenAsync(Kitchen kitchen);
        public Task UpsertUserKitchenAsync(UserKitchen kitchen);

        // UPDATES
        public Task UpdateUserInformationAsync(User user);

        // REMOVES
        public Task RemoveRecipeEquipmentRowAsync(RecipeEquipment equipment);
        public Task RemoveRecipeIngredientRowAsync(RecipeIngredients ingredient);


    }
}
