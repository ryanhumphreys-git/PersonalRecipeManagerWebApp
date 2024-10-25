using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask AddRecipeAsync(Recipe recipe);
        public ValueTask<Recipe> RetrieveRecipeByIdAsync(Guid id);
        public ValueTask UpsertRecipeAsync(Recipe recipe);
        public ValueTask RemoveRecipeAsync(Recipe recipe);

    }
}
