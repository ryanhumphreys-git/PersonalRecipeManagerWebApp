using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers;

public interface IDataBroker
{
    // INSERTS
    public Task InsertWarehouseEquipmentAsync(Equipment equipment);
    public Task InsertWarehouseIngredientsAsync(Ingredients ingredients);
    public Task InsertKitchenEquipmentAsync(KitchenEquipment equipment);
    public Task InsertKitchenIngredientAsync(KitchenIngredients ingredients);
    public Task InsertRecipeAsync(Recipe recipe);
    public Task InsertUserRecipeAsync(UserRecipes recipe);
    public Task InsertRecipeIngredientsAsync(RecipeIngredients ingredients);
    public Task InsertRecipeEquipmentAsync(RecipeEquipment equipment);
    public Task InsertKitchenAsync(Kitchen kitchen);


    // SELECTS
    public Task<KitchenEquipment> SelectKitchenEquipmentByIdAsync(Guid id);
    public Task<KitchenIngredients> SelectKitchenIngredientByIdAsync(Guid id);
    public Task<RecipeIngredients> SelectRecipeIngredientByIdAsync(Guid id);
    public Task<RecipeEquipment> SelectRecipeEquipmentByIdAsync(Guid id);
    public Task<List<Equipment>> SelectAllWarehouseEquipmentAsync();
    public Task<List<Ingredients>> SelectAllWarehouseIngredientsAsync();
    public Task<User> SelectUserInformationByIdAsync(Guid id);
    public Task<Kitchen> SelectKitchenByIdAsync(Guid id);
    public Task<List<KitchenType>> SelectAllKitchenTypesAsync();
    public Task<UserKitchen> SelectUserKitchenByIdAsync(Guid id);
    public Task<List<EquipmentDto>> SelectKitchenEquipmentDtoByKitchenIdAsync(Guid id);
    public Task<List<KitchenIngredientsDto>> SelectKitchenIngredientsDtoByKitchenIdAsync(Guid id);
    public Task<List<Recipe>> SelectUserRecipesByIdAsync(Guid id);
    public Task<List<RecipeIngredientsDto>> SelectRecipeIngredientsDtoByRecipeIdAsync(Guid id);
    public Task<List<RecipeEquipmentDto>> SelectRecipeEquipmentDtoByRecipeIdAsync(Guid id);
    public Task<Recipe> SelectRecipeByIdAsync(Guid id);
    public Task<double> SelectIngredientCost(Guid id);
    public Task<Equipment> SelectWarehouseEquipmentByIdAsync(Guid id);
    public Task<Ingredients> SelectWarehouseIngredientByIdAsync(Guid id);
    public Task<List<Kitchen>> SelectAllUserKitchensAsync(Guid id);

    // UPDATES
    public Task UpdateWarehouseEquipmentAsync(Equipment equipment);
    public Task UpdateWarehouseIngredientAsync(Ingredients ingredient);
    public Task UpdateKitchenEquipmentAsync(KitchenEquipment equipment);
    public Task UpdateKitchenIngredientAsync(KitchenIngredients ingredient);
    public Task UpdateRecipeAsync(Recipe recipe);
    public Task UpdateRecipeIngredientAsync(RecipeIngredients ingredient);
    public Task UpdateRecipeEquipmentAsync(RecipeEquipment equipment);
    public Task UpdateUserInformationAsync(User user);
    public Task UpdateKitchenAsync(Kitchen kitchen);

    // DELETES
    public Task DeleteRecipeEquipmentAsync(RecipeEquipment equipment);
    public Task DeleteRecipeIngredientAsync(RecipeIngredients ingredient);
}