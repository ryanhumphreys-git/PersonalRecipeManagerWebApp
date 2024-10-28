using Microsoft.AspNetCore.Components;
using PersonalRecipeManagerWebApp.Models;
using Radzen;
using Radzen.Blazor;

namespace PersonalRecipeManagerWebApp.Components.Pages.ManageWarehouse
{
    public partial class IngredientsWarehouse
    {
        [Inject]
        DialogService DialogService { get; set; }

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

            await InteractionService.AddWarehouseIngredientsAsync(ingredient);
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
            await InteractionService.UpsertWarehouseIngredientsAsync(ingredient);
        }

        async Task DeleteRow(Ingredients ingredient)
        {
            var result = await DialogService.Confirm("Are you sure?", "Delete Row", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (result.HasValue && result.Value)
            {
                ingredients.Remove(ingredient);
                await InteractionService.RemoveWarehouseIngredientsAsync(ingredient);
                await ingredientsGrid.Reload();
            }
            disableAdd = false;
        }
    }
}
