using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.RecipeApi;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask<bool> AddUserRecipeAsync(UserRecipes recipe)
        {
            bool isSuccessful = false;
            UserRecipes insertUserRecipes = await _broker.InsertUserRecipeAsync(recipe);
            if (insertUserRecipes is not null) isSuccessful = true;
            return isSuccessful;
        }
        public async ValueTask<User> RetrieveUserInformationByIdAsync(Guid id)
        {
            return await _broker.SelectUserInformationByIdAsync(id);
        }
        public async ValueTask<UserKitchen> RetrieveUserKitchenByIdAsync(Guid id)
        {
            return await _broker.SelectUserKitchenByIdAsync(id);
        }
        public async ValueTask<List<Recipe>> RetrieveUserRecipesByIdAsync(Guid userId, Guid recipeId)
        {
            List<UserRecipes> userRecipeInfo = await _broker.SelectAllUserRecipesAsync(userId);
            var userRecipeIds = userRecipeInfo.Select(ur => ur.RecipeId).ToList();
            List<Recipe> allRecipes = await _broker.SelectAllRecipeAsync();
            List<Recipe> userRecipes = allRecipes.Where(r => userRecipeIds.Contains(r.Id)).ToList();
            return userRecipes;
        }
        public async ValueTask<List<Kitchen>> RetrieveAllUserKitchensAsync(Guid id)
        {
            return await _broker.SelectAllUserKitchensAsync(id);
        }
        public async ValueTask UpsertUserKitchenAsync(UserKitchen kitchen)
        {

        }
        public async ValueTask<bool> UpdateUserInformationAsync(User user)
        {
            bool isSuccessful = false;
            User updateUserInformation = await _broker.UpdateUserInformationAsync(user);
            if (updateUserInformation is not null) isSuccessful = true;
            return isSuccessful;
        }
        public async ValueTask<bool> AddUserRecipeFromMealDbAsync(Guid userId, Recipe newRecipe)
        {
            bool isSuccessful = false;
            UserRecipes recipeExists = await _broker.SelectUserRecipesByIdAsync(userId, newRecipe.Id);
            if (recipeExists is null)
            {
                UserRecipes insertUserRecipes = await _broker.InsertUserRecipeAsync(new UserRecipes(userId, newRecipe.Id));
                if (insertUserRecipes is not null) isSuccessful = true;
            }
            return isSuccessful;
        }
    }
}
