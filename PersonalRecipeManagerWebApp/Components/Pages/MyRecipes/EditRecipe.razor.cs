using Microsoft.AspNetCore.Components;
using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models.Ingredients;
using Radzen;
using Radzen.Blazor;
using System.Transactions;

namespace PersonalRecipeManagerWebApp.Components.Pages.MyRecipes
{
    public partial class EditRecipe
    {
        [SupplyParameterFromQuery] public Guid RecipeId { get; set; }
        [SupplyParameterFromQuery] public Guid UserId { get; set; }
        [Parameter] public bool ShowClose { get; set; } = true;

        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] DialogService DialogService { get; set; }

        private bool disabled = true;
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

        private RecipeIngredientsViewModel currentlyEditingIngredient = new();
        private RecipeEquipmentViewModel currentlyEditingEquipment = new();


        protected override async Task OnInitializedAsync()
        {
            isLoading = true;

            recipe = await InteractionService.RetrieveRecipeByIdAsync(RecipeId);


            allIngredients = await InteractionService.RetrieveAllWarehouseIngredientsAsync();
            recipeIngredients = await InteractionService.RetrieveRecipeIngredientsDtoByRecipeIdAsync(RecipeId);
            ingredientIds = recipeIngredients.Select(e => e.Id).ToList();

            recipeEquipment = await InteractionService.RetrieveRecipeEquipmentDtoByRecipeIdAsync(RecipeId);
            allEquipment = await InteractionService.RetrieveAllWarehouseEquipmentAsync();
            equipmentIds = recipeEquipment.Select(e => e.Id).ToList();

            isLoading = false;
        }

        void OnClickEditRecipeInfo()
        {
            disabled = false;
        }

        async Task OnClickSaveRecipeInfo()
        {
            disabled = true;
            await InteractionService.UpsertRecipeAsync(recipe);
        }

        void NavigateToRecipes()
        {
            // NavigationManager.NavigateTo($"/myrecipes?userId={UserId}");
            var url = $"/myrecipes?userId={UserId}";
            Console.Write( url );
            NavigationManager.NavigateTo(url);
            // Path = "@($" / myrecipes ? userid ={ userId}")"

        }

        async Task OnCreateRowEquipment(RecipeEquipmentViewModel equipment)
        {
            disableAdd = false;
            insertingRow = true;

            RecipeEquipment newEquipment = new(RecipeId, equipment.Id, equipment.Quantity);

            bool addRecipeEquipmentSuccess = await InteractionService.AddRecipeEquipmentAsync(newEquipment);
            if (addRecipeEquipmentSuccess is false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to save {equipment.Name} to your kitchen");
            }
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

            if (currentEquipment is null)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to retrieve {equipment.Name}");
            }
            else
            {
                equipment.Name = currentEquipment.Name;

                disableAdd = false;

                if (insertingRow == true)
                {
                    equipmentIds.Add(equipment.Id);
                }

                insertingRow = false;

                await equipmentGrid.UpdateRow(equipment);
                await InvokeAsync(StateHasChanged);
            }
        }

        async Task OnUpdateRowEquipment(RecipeEquipmentViewModel equipment)
        {
            bool upsertSuccess = await InteractionService.UpsertRecipeEquipmentAsync(RecipeId, equipment);
            if (upsertSuccess is false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to insert or update {equipment.Name}");
            }
        }

        async Task DeleteEquipmentRow(RecipeEquipmentViewModel equipment)
        {
            var result = await DialogService.Confirm("Are you sure?", "Delete Row", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (result.HasValue && result.Value)
            {
                recipeEquipment.Remove(equipment);
                equipmentIds.Remove(equipment.Id);
                bool removeSuccess = await InteractionService.RemoveRecipeEquipmentAsync(RecipeId, equipment);
                if (removeSuccess is false)
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to delete {equipment.Name}");
                }
                await equipmentGrid.Reload();
            }
            disableAdd = false;
        }

        async Task OnCreateRowIngredient(RecipeIngredientsViewModel ingredient)
        {
            disableAdd = false;
            insertingRow = true;

            RecipeIngredients newIngredient = new(RecipeId, ingredient.Id, ingredient.Quantity, ingredient.UnitOfMeasurement);

            bool addSuccess = await InteractionService.AddRecipeIngredientsAsync(newIngredient);
            if (addSuccess is false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to add {ingredient.Name}");
            }
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
            if (currentIngredient is null)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to retrieve {ingredient.Name}");
            }
            ingredient.Name = currentIngredient.Name;

            disableAdd = false;

            await ingredientsGrid.UpdateRow(ingredient);
            await InvokeAsync(StateHasChanged);
        }

        async Task OnUpdateRowIngredient(RecipeIngredientsViewModel ingredient)
        {
            bool upsertSuccess = await InteractionService.UpsertRecipeIngredientAsync(RecipeId, ingredient);
            if (upsertSuccess is false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to insert or update {ingredient.Name}");
            }
        }

        async Task DeleteIngredientRow(RecipeIngredientsViewModel ingredient)
        {
            var result = await DialogService.Confirm("Are you sure?", "Delete Row", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (result.HasValue && result.Value)
            {
                recipeIngredients.Remove(ingredient);
                bool removeSuccess = await InteractionService.RemoveRecipeIngredientAsync(RecipeId, ingredient);
                if (removeSuccess is false)
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to remove {ingredient.Name}");
                }
                await ingredientsGrid.Reload();
            }
            disableAdd = false;

        }
    }
}
