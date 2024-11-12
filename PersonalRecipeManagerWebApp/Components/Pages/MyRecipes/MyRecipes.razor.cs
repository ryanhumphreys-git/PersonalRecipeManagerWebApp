using Microsoft.AspNetCore.Components;
using PersonalRecipeManagerWebApp.Models;
using Radzen.Blazor;
using Radzen;


namespace PersonalRecipeManagerWebApp.Components.Pages.MyRecipes
{
    public partial class MyRecipes
    {
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] DialogService DialogService { get; set; }
        [SupplyParameterFromQuery] private Guid UserId { get; set; }

        RadzenDataGrid<Recipe> recipeGrid;
        List<Recipe> preRecipe;
        List<Recipe> recipes;

        private Recipe currentlyEditingRecipe;

        async Task OpenRecipe(Guid recipeId)
        {
            await DialogService.OpenAsync<RecipeInfoCard>($"Recipe Information",
                new Dictionary<string, object>() { { "RecipeId", recipeId } },
                new DialogOptions() { Width = "700px", Height = "700px" });
            
        }
        protected override async Task OnInitializedAsync()
        {
            // TODO: figure out how to address errors in oninitializedasync
            preRecipe = await InteractionService.RetrieveAllUserRecipesByUserIdAsync(UserId);
            recipes = await InteractionService.RetrieveRecipeCost(preRecipe);

            await base.OnInitializedAsync();
        }
        void OpenAddNewRecipe()
        {
            NavigationManager.NavigateTo($"/myrecipes/editrecipe?UserId={UserId}&isnew=true");
        }
        void OpenEditRecipe(Recipe recipe)
        {
            NavigationManager.NavigateTo($"/myrecipes/editrecipe?RecipeId={recipe.Id}&UserId={UserId}");
        }
        async Task DeleteRow(Recipe recipe)
        {
            var result = await DialogService.Confirm("Are you sure?", "Delete Row", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (result.HasValue && result.Value)
            {
                recipes.Remove(recipe);
                bool removeSuccess = await InteractionService.RemoveRecipeAsync(recipe);
                if (removeSuccess is false)
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to remove {recipe.Name} from your kitchen");
                }
                await recipeGrid.Reload();
            }
        }
    }
}
