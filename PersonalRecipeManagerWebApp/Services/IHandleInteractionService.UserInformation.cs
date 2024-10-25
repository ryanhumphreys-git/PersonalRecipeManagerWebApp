using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask AddUserRecipeAsync(UserRecipes recipe);
        public ValueTask<User> RetrieveUserInformationByIdAsync(Guid id);
        public ValueTask<UserKitchen> RetrieveUserKitchenByIdAsync(Guid id);
        public ValueTask<List<Kitchen>> RetrieveAllUserKitchensAsync(Guid id);
        public ValueTask UpsertUserKitchenAsync(UserKitchen kitchen);
        public ValueTask UpdateUserInformationAsync(User user);
        public ValueTask<List<Recipe>> RetrieveUserRecipesByIdAsync(Guid id);

    }
}
