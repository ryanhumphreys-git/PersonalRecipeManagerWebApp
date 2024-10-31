using Microsoft.AspNetCore.Components;
using PersonalRecipeManagerWebApp.Models;
using Radzen.Blazor;
using Radzen;


namespace PersonalRecipeManagerWebApp.Components.Pages.MyRecipes
{
    public partial class MyRecipes
    {
        [Inject] DialogService DialogService { get; set; }
        [SupplyParameterFromQuery]
        private Guid Id { get; set; }
        RadzenDataGrid<Recipe> recipeGrid;
        List<Recipe> preRecipe;
        List<Recipe> recipes;

        private bool disableAdd;
        private bool isDialogOpen = false;

        private Recipe currentlyEditingRecipe;

        async Task OpenRecipe(Guid recipeId)
        {
            await DialogService.OpenAsync<RecipeInfoCard>($"Recipe Information",
                new Dictionary<string, object>() { { "RecipeId", recipeId } },
                new DialogOptions() { Width = "700px", Height = "700px" });
            
        }


                    

        protected override async Task OnInitializedAsync()
        {
            preRecipe = await InteractionService.RetrieveAllUserRecipesByUserIdAsync(Id);
            recipes = await InteractionService.RetrieveRecipeCost(preRecipe);

            await base.OnInitializedAsync();
        }

        async Task OnCreateRow(Recipe recipe)
        {
            disableAdd = false;

            UserRecipes userRecipes = new(Id, recipe.Id);

            await InteractionService.AddRecipeAsync(recipe);
            await InteractionService.AddUserRecipeAsync(userRecipes);
        }

        async Task InsertRow()
        {
            disableAdd = true;
            var recipe = new Recipe(Guid.NewGuid());
            await recipeGrid.InsertRow(recipe);
        }
        void CancelEdit(Recipe recipe)
        {
            if (currentlyEditingRecipe is not null)
            {
                recipe.Name = currentlyEditingRecipe.Name;
                recipe.Difficulty = currentlyEditingRecipe.Difficulty;
                recipe.Time = currentlyEditingRecipe.Time;
            }

            recipeGrid.CancelEditRow(recipe);

            disableAdd = false;
        }
        async Task EditRow(Recipe recipe)
        {
            currentlyEditingRecipe = new(recipe.Id, recipe.Name, recipe.Difficulty, recipe.Time);

            disableAdd = true;

            await recipeGrid.EditRow(recipe);
        }

        async Task SaveRow(Recipe recipe)
        {
            await recipeGrid.UpdateRow(recipe);
        }

        async Task OnUpdateRow(Recipe recipe)
        {
            disableAdd = false;
            await InteractionService.UpsertRecipeAsync(recipe);
        }

        async Task DeleteRow(Recipe recipe)
        {
            var result = await DialogService.Confirm("Are you sure?", "Delete Row", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (result.HasValue && result.Value)
            {
                recipes.Remove(recipe);
                await InteractionService.RemoveRecipeAsync(recipe);
                await recipeGrid.Reload();
            }
            disableAdd = false;
        }
    }
}
