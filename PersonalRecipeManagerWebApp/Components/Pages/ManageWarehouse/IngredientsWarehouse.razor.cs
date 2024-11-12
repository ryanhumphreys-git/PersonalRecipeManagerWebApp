using Microsoft.AspNetCore.Components;
using PersonalRecipeManagerWebApp.Components.Pages.MyKitchen;
using PersonalRecipeManagerWebApp.Models.Ingredients;
using Radzen;
using Radzen.Blazor;

namespace PersonalRecipeManagerWebApp.Components.Pages.ManageWarehouse
{
    public partial class IngredientsWarehouse
    {
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] DialogService DialogService { get; set; }

        RadzenDataGrid<Ingredients> ingredientsGrid;
        List<Ingredients> ingredients;

        private Ingredients currentlyEditingIngredient;

        private bool disableAdd;
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            ingredients = await InteractionService.RetrieveAllWarehouseIngredientsAsync();
        }

        async Task OnCreateRow(Ingredients ingredient)
        {
            disableAdd = false;

            bool addWarhouseIngredientsSuccess = await InteractionService.AddWarehouseIngredientsAsync(ingredient);
            if (addWarhouseIngredientsSuccess is false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to retrieve {ingredient.Name})");
            }
        }

        async Task InsertRow()
        {
            disableAdd = true;
            var ingredient = new Ingredients(Guid.NewGuid());
            await ingredientsGrid.InsertRow(ingredient);
        }
        void CancelEdit(Ingredients ingredient)
        {
            if (currentlyEditingIngredient is not null)
            {
                ingredient.Name = currentlyEditingIngredient.Name;
                ingredient.Cost = currentlyEditingIngredient.Cost;
                ingredient.UnitOfMeasurement = currentlyEditingIngredient.UnitOfMeasurement;
            }

            disableAdd = false;

            ingredientsGrid.CancelEditRow(ingredient);
        }
        async Task EditRow(Ingredients ingredient)
        {
            currentlyEditingIngredient = new(ingredient.Id, ingredient.Name, ingredient.Cost, ingredient.UnitOfMeasurement);

            disableAdd = true;

            await ingredientsGrid.EditRow(ingredient);
        }

        async Task SaveRow(Ingredients ingredient)
        {
            disableAdd = false;
            await ingredientsGrid.UpdateRow(ingredient);
        }

        async Task OnUpdateRow(Ingredients ingredient)
        {
            bool upsertWarehouseIngredientSuccess = await InteractionService.UpsertWarehouseIngredientsAsync(ingredient);
            if (upsertWarehouseIngredientSuccess is false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to insert or update {ingredient.Name})");
            }
        }

        async Task DeleteRow(Ingredients ingredient)
        {
            var result = await DialogService.Confirm("Are you sure?", "Delete Row", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (result.HasValue && result.Value)
            {
                ingredients.Remove(ingredient);
                bool removeWarehouseIngredientsSueccess = await InteractionService.RemoveWarehouseIngredientsAsync(ingredient);
                if (removeWarehouseIngredientsSueccess is false)
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to remove {ingredient.Name})");
                }
                await ingredientsGrid.Reload();
            }
            disableAdd = false;
        }
    }
}
