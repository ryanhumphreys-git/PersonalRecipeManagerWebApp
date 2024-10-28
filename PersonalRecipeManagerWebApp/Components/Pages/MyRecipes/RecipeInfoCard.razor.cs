using Microsoft.AspNetCore.Components;
using PersonalRecipeManagerWebApp.Models;
using Radzen;
using Radzen.Blazor;

namespace PersonalRecipeManagerWebApp.Components.Pages.MyRecipes
{
    public partial class RecipeInfoCard
    {
        [Parameter] public Guid RecipeId { get; set; }
        [Parameter] public bool ShowClose { get; set; } = true;

        [Inject] DialogService DialogService { get; set; }

        private bool isLoading = true;
        private bool disableAdd = false;
        private bool insertingRow = false;

        Recipe recipe;

        List<RecipeIngredientsViewModel> recipeIngredients;
        List<Ingredients> allIngredients;
        List<Guid> ingredientIds;

        List<RecipeEquipmentViewModel> recipeEquipment;
        List<Equipment> allEquipment;
        List<Guid> equipmentIds;

        RadzenDataGrid<RecipeEquipmentViewModel> equipmentGrid;
        RadzenDataGrid<RecipeIngredientsViewModel> ingredientsGrid;

        private RecipeIngredientsViewModel currentlyEditingIngredient;
        private RecipeEquipmentViewModel currentlyEditingEquipment;


        protected override async Task OnParametersSetAsync()
        {
            isLoading = true;

            recipe = await InteractionService.RetrieveRecipeByIdAsync(RecipeId);

            recipeIngredients = await InteractionService.RetrieveRecipeIngredientsDtoByRecipeIdAsync(RecipeId);
            allIngredients = await InteractionService.RetrieveAllWarehouseIngredientsAsync();
            ingredientIds = recipeIngredients.Select(e => e.Id).ToList();

            recipeEquipment = await InteractionService.RetrieveRecipeEquipmentDtoByRecipeIdAsync(RecipeId);
            allEquipment = await InteractionService.RetrieveAllWarehouseEquipmentAsync();
            equipmentIds = recipeEquipment.Select(e => e.Id).ToList();

            isLoading = false;

            await base.OnParametersSetAsync();
        }

        async Task OnCreateRowEquipment(RecipeEquipmentViewModel equipment)
        {
            disableAdd = false;
            insertingRow = true;

            RecipeEquipment newEquipment = new(RecipeId, equipment.Id, equipment.Quantity);

            await InteractionService.AddRecipeEquipmentAsync(newEquipment);
        }

        async Task InsertRowEquipment()
        {
            insertingRow = true;
            disableAdd = true;
            var equipment = new RecipeEquipmentViewModel();
            await equipmentGrid.InsertRow(equipment);
        }

        void CancelEditEquipment(RecipeEquipmentViewModel equipment)
        {
            if (currentlyEditingEquipment is not null)
            {
                equipment.Name = currentlyEditingEquipment.Name;
                equipment.Quantity = currentlyEditingEquipment.Quantity;
            }

            disableAdd = false;
            insertingRow = true;

            equipmentGrid.CancelEditRow(equipment);
        }

        async Task EditRowEquipment(RecipeEquipmentViewModel equipment)
        {
            insertingRow = false;
            currentlyEditingEquipment = new()
            {
                Id = equipment.Id,
                Name = equipment.Name,
                Quantity = equipment.Quantity,
            };

            disableAdd = true;

            await equipmentGrid.EditRow(equipment);
        }

        async Task SaveRowEquipment(RecipeEquipmentViewModel equipment)
        {
            Equipment currentEquipment = await InteractionService.RetrieveWarehouseEquipmentByIdAsync(equipment.Id);
            equipment.Name = currentEquipment.Name;

            disableAdd = false;

            if (insertingRow == true)
            {
                equipmentIds.Add(equipment.Id);
            }

            insertingRow = false;
            await equipmentGrid.UpdateRow(equipment);
        }

        async Task OnUpdateRowEquipment(RecipeEquipmentViewModel equipment)
        {
            await InteractionService.UpsertRecipeEquipmentAsync(RecipeId, equipment);
        }

        async Task DeleteEquipmentRow(RecipeEquipmentViewModel equipment)
        {
            var result = await DialogService.Confirm("Are you sure?", "Delete Row", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (result.HasValue && result.Value)
            {
                recipeEquipment.Remove(equipment);
                equipmentIds.Remove(equipment.Id);
                await InteractionService.RemoveRecipeEquipmentAsync(RecipeId, equipment);
                await equipmentGrid.Reload();
            }
            disableAdd = false;
        }

        async Task OnCreateRowIngredient(RecipeIngredientsViewModel ingredient)
        {
            disableAdd = false;
            insertingRow = true;

            RecipeIngredients newIngredient = new(RecipeId, ingredient.Id, ingredient.Quantity, ingredient.UnitOfMeasurement);

            await InteractionService.AddRecipeIngredientsAsync(newIngredient);
        }

        async Task InsertRowIngredient()
        {
            disableAdd = true;
            insertingRow = true;

            var ingredient = new RecipeIngredientsViewModel();
            await ingredientsGrid.InsertRow(ingredient);
        }
        void CancelEditIngredient(RecipeIngredientsViewModel ingredient)
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
        async Task EditRowIngredient(RecipeIngredientsViewModel ingredient)
        {
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

        async Task SaveRowIngredient(RecipeIngredientsViewModel ingredient)
        {
            Ingredients currentIngredient = await InteractionService.RetrieveWarehouseIngredientByIdAsync(ingredient.Id);
            ingredient.Name = currentIngredient.Name;

            disableAdd = false;

            await ingredientsGrid.UpdateRow(ingredient);
        }

        async Task OnUpdateRowIngredient(RecipeIngredientsViewModel ingredient)
        {
            await InteractionService.UpsertRecipeIngredientAsync(RecipeId, ingredient);
        }

        async Task DeleteIngredientRow(RecipeIngredientsViewModel ingredient)
        {
            var result = await DialogService.Confirm("Are you sure?", "Delete Row", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (result.HasValue && result.Value)
            {
                recipeIngredients.Remove(ingredient);
                await InteractionService.RemoveRecipeIngredientAsync(RecipeId, ingredient);
                await ingredientsGrid.Reload();
            }
            disableAdd = false;
            
        }
    }
}
