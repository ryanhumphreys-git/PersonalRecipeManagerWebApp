using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.RecipeApi;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask AddRecipeAsync(Recipe recipe)
        {
            await _broker.InsertRecipeAsync(recipe);
        }
        public async ValueTask<Recipe> RetrieveRecipeByIdAsync(Guid id)
        {

            return await _broker.SelectRecipeByIdAsync(id);
        }
        public async ValueTask UpsertRecipeAsync(Recipe recipe)
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
        public async ValueTask RemoveRecipeAsync(Recipe recipe)
        {
            await _broker.DeleteUserRecipesByIdAsync(recipe.Id);
            await _broker.DeleteRecipeAsync(recipe);
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
