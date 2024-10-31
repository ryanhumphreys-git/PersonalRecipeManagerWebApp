using Microsoft.AspNetCore.Components;
using PersonalRecipeManagerWebApp.Models.RecipeApi;
using PersonalRecipeManagerWebApp.Services.ApiCallHandler;
using PersonalRecipeManagerWebApp.Services;
using Radzen.Blazor;
using PersonalRecipeManagerWebApp.Models;
using System.Data.SqlTypes;
using Radzen;

namespace PersonalRecipeManagerWebApp.Components.Pages.MyRecipes
{
    public partial class RecipeSelect
    {
        [Inject] NotificationService NotificationService { get; set; }
        [SupplyParameterFromQuery]
        private Guid UserId { get; set; }
        [Inject] IHandleMealDbApi mealService { get; set; }
        [Inject] IHandleInteractionService storageService { get; set; }
        private IList<MealsDbSearchCleaned> recipes { get; set; }
        private string searchValue { get; set; }
        private IList<MealsDbSearchCleaned> selectedRecipe;
        private RadzenDataGrid<MealsDbSearchCleaned> searchResultGrid;

        private async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private async ValueTask OnClick()
        {
            try
            {
                recipes = await mealService.GetMealDbSearchResultsByQueryExpression(searchValue);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"{ex.Message}");
            }
            StateHasChanged();
            await Task.Delay(10);
            await searchResultGrid.Reload();
        }

        private async ValueTask OnClickConfirm()
        {
            if (selectedRecipe is null || selectedRecipe.Count() == 0)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Error",
                    Detail = "No recipe selected",
                    Duration = 6000
                });
                return;
            }
            MealsDbSearchCleaned recipeSelected = selectedRecipe[0];
            bool recipeExists = await storageService.CheckIfRecipeExistsByName(recipeSelected.Name);
            if (recipeExists)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Warning,
                    Summary = "Alert",
                    Detail = "You already know this recipe",
                    Duration = 6000
                });
            }
            else
            {
                Recipe selectedRecipeType = await storageService.AddRecipeFromMealDb(selectedRecipe);
                await storageService.AddWarehouseIngredientFromMealDbAsync(recipeSelected);
                await storageService.AddRecipeIngredientsFromMealDbAsync(recipeSelected, selectedRecipeType);
                await storageService.AddUserRecipeFromMealDbAsync(UserId, selectedRecipeType);

                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Alert",
                    Detail = "Recipe has been added",
                    Duration = 6000
                });
            }
        }
    }
}
