using Microsoft.AspNetCore.Components;
using PersonalRecipeManagerWebApp.Models;
using Radzen;
using Radzen.Blazor;
using System.ComponentModel;

namespace PersonalRecipeManagerWebApp.Components.Pages.MyKitchen
{
    public partial class IngredientsInventory
    {
        private Guid kitchenId = new Guid("E7C14A98-BF68-4D93-A05E-EAD425347E9F");
        [SupplyParameterFromQuery]
        private Guid Id { get; set; }

        [Inject] DialogService DialogService { get; set; }
        RadzenDataGrid<KitchenIngredientsViewModel> ingredientsGrid;
        List<KitchenIngredientsViewModel> userIngredients;
        List<Ingredients> allIngredients;
        List<Guid> ingredientIds;
        private User user { get; set; }

        private KitchenIngredientsViewModel currentlyEditingIngredient;

        private bool disableAdd;
        private bool insertingRow;

        private List<Ingredients> FilteredIngredientList =>
            allIngredients.Where(e => !ingredientIds.Contains(e.Id)).ToList();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            user = await InteractionService.RetrieveUserInformationByIdAsync(Id);

            userIngredients = await InteractionService.RetrieveKitchenIngredientsDtoByKitchenIdAsync(kitchenId);
            ingredientIds = userIngredients.Select(e => e.Id).ToList();

            allIngredients = await InteractionService.RetrieveAllWarehouseIngredientsAsync();
        }

        void OnSelectionChange(object value, KitchenIngredientsViewModel ingredient)
        {
            if (value is Guid selectedId)
            {
                var selectedIngredient = FilteredIngredientList.FirstOrDefault(i => i.Id == selectedId);
                if (selectedIngredient != null)
                {
                    ingredient.UnitOfMeasurement = selectedIngredient.UnitOfMeasurement;
                    StateHasChanged();
                }
            }
        }

        async Task OnCreateRow(KitchenIngredientsViewModel ingredient)
        {
            disableAdd = false;
            insertingRow = true;

            KitchenIngredients newIngredient = new(kitchenId, ingredient.Id, ingredient.Quantity);

            await InteractionService.AddKitchenIngredientsAsync(newIngredient);
        }

        async Task InsertRow()
        {
            insertingRow = true;
            disableAdd = true;
            var ingredient = new KitchenIngredientsViewModel();
            await ingredientsGrid.InsertRow(ingredient);
        }
        void CancelEdit(KitchenIngredientsViewModel ingredient)
        {
            if (currentlyEditingIngredient is not null)
            {
                ingredient.Name = currentlyEditingIngredient.Name;
                ingredient.Quantity = currentlyEditingIngredient.Quantity;
                ingredient.UnitOfMeasurement = currentlyEditingIngredient.UnitOfMeasurement;
            }

            disableAdd = false;
            insertingRow = false;

            ingredientsGrid.CancelEditRow(ingredient);
        }

        async Task EditRow(KitchenIngredientsViewModel ingredient)
        {
            insertingRow = false;
            currentlyEditingIngredient = new()
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Quantity = ingredient.Quantity,
                UnitOfMeasurement = ingredient.UnitOfMeasurement
            };

            disableAdd = true;

            await ingredientsGrid.EditRow(ingredient);
        }

        async Task SaveRow(KitchenIngredientsViewModel ingredient)
        {
            Ingredients currentIngredient = await InteractionService.RetrieveWarehouseIngredientByIdAsync(ingredient.Id);
            ingredient.Name = currentIngredient.Name;
            ingredient.UnitOfMeasurement = currentIngredient.UnitOfMeasurement;

            if (insertingRow == true)
            {
                ingredientIds.Add(ingredient.Id);
            } 
            disableAdd = false;

            await ingredientsGrid.UpdateRow(ingredient);
            
            
        }

        async Task OnUpdateRow(KitchenIngredientsViewModel ingredient)
        {
            await InteractionService.UpsertKitchenIngredientsAsync(kitchenId, ingredient);
        }

        async Task DeleteRow(KitchenIngredientsViewModel ingredient)
        {
            var result = await DialogService.Confirm("Are you sure?", "Delete Row", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (result.HasValue && result.Value)
            {
                userIngredients.Remove(ingredient);
                ingredientIds.Remove(ingredient.Id);
                await InteractionService.RemoveKitchenIngredientsAsync(kitchenId, ingredient);
                await ingredientsGrid.Reload();
            }
            disableAdd = false;
        }
    }
}
