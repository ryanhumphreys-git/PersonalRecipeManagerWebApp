using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.RecipeApi;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask<bool> AddRecipeAsync(Recipe recipe)
        {
            bool isSuccessful = false;
            Recipe insertRecipe = await _broker.InsertRecipeAsync(recipe);
            if (insertRecipe is not null) isSuccessful = true;
            return isSuccessful;
        }
        public async ValueTask<Recipe> RetrieveRecipeByIdAsync(Guid id)
        {

            return await _broker.SelectRecipeByIdAsync(id);
        }
        public async ValueTask<bool> UpsertRecipeAsync(Recipe recipe)
        {
            bool isSuccessful = false;
            var recipeExists = await _broker.SelectRecipeByIdAsync(recipe.Id);

            if (recipeExists is null)
            {
                Recipe insertRecipe = await _broker.InsertRecipeAsync(recipe);
                if (insertRecipe is not null) isSuccessful = true;
            }
            else
            {
                Recipe updateRecipe = await _broker.UpdateRecipeAsync(recipe);
                if (updateRecipe is not null) isSuccessful = true;
            }
            return isSuccessful;
        }
        public async ValueTask<bool> RemoveRecipeAsync(Recipe recipe)
        {
            bool isSuccessful = false;
            UserRecipes deleteUserRecipe = await _broker.DeleteUserRecipesByIdAsync(recipe.Id);
            if (deleteUserRecipe is null) return false;
            Recipe deleteRecipe = await _broker.DeleteRecipeAsync(recipe);
            if (deleteUserRecipe is not null && deleteRecipe is not null) isSuccessful = true;
            return isSuccessful;
        }
        public Recipe ConvertMealDbRecipeIntoRecipeType(MealsDbSearchCleaned mealDbRecipe)
        {
            return new Recipe(Guid.NewGuid(), mealDbRecipe.Name, 0, 0);
        }
        public async ValueTask<bool> CheckIfRecipeExistsByName(string name)
        {
            Recipe recipeByName = await _broker.SelectRecipeByNameAsync(name);
            if (recipeByName is null)
            {
                return false;
            }
            return true;
        }
        public async ValueTask<Recipe> AddRecipeFromMealDb(IList<MealsDbSearchCleaned> mealDbSearchResults)
        {
            MealsDbSearchCleaned recipeSelection = mealDbSearchResults[0];
            Recipe selectedRecipeType = ConvertMealDbRecipeIntoRecipeType(recipeSelection);
            Recipe recipeByName = await _broker.SelectRecipeByNameAsync(recipeSelection.Name);
            if (recipeByName is null)
            {
                await _broker.InsertRecipeAsync(selectedRecipeType);
            }
            if (recipeByName is not null)
            {
                selectedRecipeType.Id = recipeByName.Id;
                selectedRecipeType.Difficulty = recipeByName.Difficulty;
                selectedRecipeType.Time = recipeByName.Time;
            }
            return selectedRecipeType;
        }
        public async ValueTask InsertUserRecipeFromMealDb(Guid userId, Recipe recipe)
        {
            UserRecipes newRecipe = new UserRecipes();
            newRecipe.UserId = userId;
            newRecipe.RecipeId = recipe.Id;
            await _broker.InsertUserRecipeAsync(newRecipe);
        }
        public async ValueTask<List<Recipe>> RetrieveAllUserRecipesByUserIdAsync(Guid userId)
        {
            List<UserRecipes> userRecipes = await _broker.SelectAllUserRecipesAsync(userId);
            List<Recipe> recipeList = new();
            foreach (UserRecipes recipes in userRecipes)
            {
                recipeList.Add(await _broker.SelectRecipeByIdAsync(recipes.RecipeId.Value));
            }
            return recipeList;
        }

    }
}
