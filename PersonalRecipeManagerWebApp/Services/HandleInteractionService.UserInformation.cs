using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService
    {
        public async ValueTask AddUserRecipeAsync(UserRecipes recipe)
        {
            await _broker.InsertUserRecipeAsync(recipe);
        }
        public async ValueTask<User> RetrieveUserInformationByIdAsync(Guid id)
        {
            return await _broker.SelectUserInformationByIdAsync(id);
        }
        public async ValueTask<UserKitchen> RetrieveUserKitchenByIdAsync(Guid id)
        {

            return await _broker.SelectUserKitchenByIdAsync(id);
        }
        public async ValueTask<List<Recipe>> RetrieveUserRecipesByIdAsync(Guid userId)
        {
            List<UserRecipes> userRecipeInfo = await _broker.SelectUserRecipesByIdAsync(userId);
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
        public async ValueTask UpdateUserInformationAsync(User user)
        {
            await _broker.UpdateUserInformationAsync(user);
        }

    }
}
