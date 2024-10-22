using Microsoft.EntityFrameworkCore;
using PersonalRecipeManagerWebApp.Data;
using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers;

public class SqlDataBroker(RecipeContext recipeContext) : IDataBroker
{
    private readonly RecipeContext db = recipeContext;

    // INSERTS
    public async Task InsertWarehouseEquipmentAsync(Equipment equipment)
    {
        await db.AddAsync(equipment);
        await db.SaveChangesAsync();
    }
    public async Task InsertWarehouseIngredientsAsync(Ingredients ingredients)
    {

        await db.AddAsync(ingredients);
        await db.SaveChangesAsync();
    }
    public async Task InsertKitchenEquipmentAsync(KitchenEquipment equipment)
    {

        await db.AddAsync(equipment);
        await db.SaveChangesAsync();
    }
    public async Task InsertKitchenIngredientAsync(KitchenIngredients ingredients)
    {
        await db.AddAsync(ingredients);
        await db.SaveChangesAsync();
    }
    public async Task InsertRecipeAsync(Recipe recipe)
    {
        await db.AddAsync(recipe);
        await db.SaveChangesAsync();
    }
    public async Task InsertUserRecipeAsync(UserRecipes recipe)
    {
        await db.AddAsync(recipe);
        await db.SaveChangesAsync();
    }
    public async Task InsertRecipeIngredientsAsync(RecipeIngredients ingredients)
    {
        await db.AddAsync(ingredients);
        await db.SaveChangesAsync();
    }
    public async Task InsertRecipeEquipmentAsync(RecipeEquipment equipment)
    {
        await db.AddAsync(equipment);
        await db.SaveChangesAsync();
    }
    public async Task InsertKitchenAsync(Kitchen kitchen)
    {
        await db.AddAsync(kitchen);
        await db.SaveChangesAsync();
    }

    // SELECTS
    public async Task<KitchenEquipment> SelectKitchenEquipmentByIdAsync(Guid id)
    {
        return await db.KitchenEquipment.Where(ke => ke.KitchenId == id).FirstOrDefaultAsync();
    }
    public async Task<KitchenIngredients> SelectKitchenIngredientByIdAsync(Guid id)
    {
        return await db.KitchenIngredients.Where(ki => ki.KitchenId == id).FirstOrDefaultAsync();
    }
    public async Task<RecipeIngredients> SelectRecipeIngredientByIdAsync(Guid id)
    {
        return await db.RecipeIngredients.Where(ri => ri.RecipeId == id).FirstOrDefaultAsync();
    }
    public async Task<RecipeEquipment> SelectRecipeEquipmentByIdAsync(Guid id)
    {
        return await db.RecipeEquipment.Where(re => re.RecipeId == id).FirstOrDefaultAsync();
    }
    public async Task<List<Equipment>> SelectAllWarehouseEquipmentAsync()
    {
        return await db.Equipment.ToListAsync();
    }
    public async Task<List<Ingredients>> SelectAllWarehouseIngredientsAsync()
    {

        return await db.Ingredients.ToListAsync();
    }
    public async Task<User> SelectUserInformationByIdAsync(Guid id)
    {

        return await db.User.Where(u => u.Id == id).FirstOrDefaultAsync();
    }
    public async Task<Kitchen> SelectKitchenByIdAsync(Guid id)
    {

        return await db.Kitchen.Where(ut => ut.Id == id).FirstOrDefaultAsync();
    }
    public async Task<List<KitchenType>> SelectAllKitchenTypesAsync()
    {

        return await db.KitchenType.ToListAsync();
    }
    public async Task<UserKitchen> SelectUserKitchenByIdAsync(Guid id)
    {

        return await db.UserKitchens.Where(uk => uk.UserId == id).FirstOrDefaultAsync();
    }
    public async Task<List<EquipmentDto>> SelectKitchenEquipmentDtoByKitchenIdAsync(Guid id)
    {

        return await db.Kitchen
                .Where(k => k.Id == id)
                .SelectMany(k => k.KitchenEquipment)
                .Select(k => new EquipmentDto
                {
                    Id = k.Equipment!.Id,
                    Name = k.Equipment.Name,
                    Quantity = k.Quantity
                })
                .ToListAsync();
    }
    public async Task<List<KitchenIngredientsDto>> SelectKitchenIngredientsDtoByKitchenIdAsync(Guid id)
    {

        return await db.Kitchen
            .Where(k => k.Id == id)
            .SelectMany(k => k.KitchenIngredients)
            .Select(k => new KitchenIngredientsDto
            {
                Id = k.Ingredient.Id,
                Name = k.Ingredient.Name,
                Quantity = k.Quantity,
                UnitOfMeasurement = k.Ingredient.UnitOfMeasurement
            })
            .ToListAsync();
    }
    public async Task<List<Recipe>> SelectUserRecipesByIdAsync(Guid id)
    {

        return await db.Recipes
                .Include(r => r.UserRecipes)
                .Where(r => r.UserRecipes.Any(ur => ur.UserId == id))
                .ToListAsync();
    }
    public async Task<List<RecipeIngredientsDto>> SelectRecipeIngredientsDtoByRecipeIdAsync(Guid id)
    {

        return await db.Recipes
            .Where(r => r.Id == id)
            .SelectMany(r => r.RecipeIngredients)
            .Select(r => new RecipeIngredientsDto
            {
                Id = r.Ingredient!.Id,
                Name = r.Ingredient.Name,
                Quantity = r.Quantity,
                UnitOfMeasurement = r.Ingredient.UnitOfMeasurement
            })
            .ToListAsync();
    }
    public async Task<List<RecipeEquipmentDto>> SelectRecipeEquipmentDtoByRecipeIdAsync(Guid id)
    {

        return await db.Recipes
            .Where(r => r.Id == id)
            .SelectMany(r => r.RecipeEquipment)
            .Select(r => new RecipeEquipmentDto
            {
                Id = r.Equipment!.Id,
                Name = r.Equipment.Name,
                Quantity = r.Quantity
            })
            .ToListAsync();
    }
    public async Task<Recipe> SelectRecipeByIdAsync(Guid id)
    {

        return await db.Recipes.Where(r => r.Id == id).FirstOrDefaultAsync();
    }
    public async Task<double> SelectIngredientCost(Guid id)
    {
        return await db.Ingredients
            .Where(i => i.Id == id)
            .Select(i => i.Cost)
            .FirstOrDefaultAsync();
    }
    public async Task<Equipment> SelectWarehouseEquipmentByIdAsync(Guid id)
    {
        return await db.Equipment.Where(e => e.Id == id).FirstOrDefaultAsync();
    }
    public async Task<Ingredients> SelectWarehouseIngredientByIdAsync(Guid id)
    {
        return await db.Ingredients.Where(e => e.Id == id).FirstOrDefaultAsync();
    }
    public async Task<List<Kitchen>> SelectAllUserKitchensAsync(Guid id)
    {
        return await db.Kitchen
            .Include(k => k.UserKitchens.Where(uk => uk.UserId == id))
            .ToListAsync();
    }


    // UPDATES
    public async Task UpdateWarehouseEquipmentAsync(Equipment equipment)
    {
        db.Update(equipment);
        await db.SaveChangesAsync();
    }
    public async Task UpdateWarehouseIngredientAsync(Ingredients ingredient)
    {
        db.Update(ingredient);
        await db.SaveChangesAsync();
    }
    public async Task UpdateKitchenEquipmentAsync(KitchenEquipment equipment)
    {
        db.Update(equipment);
        await db.SaveChangesAsync();
    }
    public async Task UpdateKitchenIngredientAsync(KitchenIngredients ingredient)
    {
        db.Update(ingredient);
        await db.SaveChangesAsync();
    }
    public async Task UpdateRecipeAsync(Recipe recipe)
    {
        db.Update(recipe);
        await db.SaveChangesAsync();
    }
    public async Task UpdateRecipeIngredientAsync(RecipeIngredients ingredient)
    {
        db.Update(ingredient);
        await db.SaveChangesAsync();
    }
    public async Task UpdateRecipeEquipmentAsync(RecipeEquipment equipment)
    {
        db.Update(equipment);
        await db.SaveChangesAsync();
    }
    public async Task UpdateUserInformationAsync(User user)
    {
        db.Update(user);
        await db.SaveChangesAsync();
    }
    public async Task UpdateKitchenAsync(Kitchen kitchen)
    {
        db.Update(kitchen);
        await db.SaveChangesAsync();
    }

    // DELETES
    public async Task DeleteRecipeEquipmentAsync(RecipeEquipment equipment)
    {
        db.RecipeEquipment.Remove(equipment);
        await db.SaveChangesAsync();
    }
    public async Task DeleteRecipeIngredientAsync(RecipeIngredients ingredient)
    {
        db.RecipeIngredients.Remove(ingredient);
        await db.SaveChangesAsync();
    }
}