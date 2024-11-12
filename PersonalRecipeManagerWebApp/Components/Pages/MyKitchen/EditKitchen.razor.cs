using Microsoft.AspNetCore.Components;
using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models.Ingredients;
using PersonalRecipeManagerWebApp.Services;
using Radzen;
using Radzen.Blazor;

namespace PersonalRecipeManagerWebApp.Components.Pages.MyKitchen
{
    public partial class EditKitchen
    {
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] DialogService DialogService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IHandleInteractionService InteractionService { get; set; }
        [SupplyParameterFromQuery] public Guid KitchenId { get; set; }

        RadzenDataGrid<KitchenEquipmentViewModel> equipmentGrid;
        List<KitchenEquipmentViewModel> userEquipment;
        List<Equipment> allEquipment;
        List<Guid> equipmentIds;

        RadzenDataGrid<KitchenIngredientsViewModel> ingredientsGrid;
        List<KitchenIngredientsViewModel> userIngredients;
        List<Ingredients> allIngredients;
        List<Guid> ingredientIds;

        private KitchenEquipmentViewModel currentlyEditingEquipment;
        private KitchenIngredientsViewModel currentlyEditingIngredient;

        private Kitchen selectedKitchen;
        private User user { get; set; }

        private bool isLoading = true;
        private bool disableAdd;
        private bool insertingRow;

        private bool kitchenIngredientsFound = true;
        private bool kitchenEquipmentFound = true;
        private bool allIngredientsFound = true;
        private bool allEquipmentFound = true;

        private List<Ingredients> FilteredIngredientList =>
            allIngredients.Where(e => !ingredientIds.Contains(e.Id)).ToList();
        private List<Equipment> FilteredEquipmentList =>
            allEquipment.Where(e => !equipmentIds.Contains(e.Id)).ToList();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            selectedKitchen = await InteractionService.RetrieveKitchenByIdAsync(KitchenId);

            userIngredients = await InteractionService.RetrieveKitchenIngredientsViewModelByKitchenIdAsync(KitchenId);
            if (userIngredients is null)
            {
                kitchenIngredientsFound = false;
            }
            else
            {
                ingredientIds = userIngredients.Select(e => e.Id).ToList();
            }

            allIngredients = await InteractionService.RetrieveAllWarehouseIngredientsAsync();
            if (allIngredients is null)
            {
                allIngredientsFound = false;
            }

            userEquipment = await InteractionService.RetrieveKitchenEquipmentDtoByKitchenIdAsync(KitchenId);
            if (userEquipment is null)
            {
                kitchenEquipmentFound = false;
            }
            else
            {
                equipmentIds = userEquipment.Select(e => e.Id).ToList();
            }

            allEquipment = await InteractionService.RetrieveAllWarehouseEquipmentAsync();
            if (allEquipment is null)
            {
                allEquipmentFound = false;
            }

            isLoading = false;
        }

        void OnIngredientSelectionChange(object value, KitchenIngredientsViewModel ingredient)
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

        async Task OnCreateIngredientRow(KitchenIngredientsViewModel ingredient)
        {
            disableAdd = false;
            insertingRow = true;

            KitchenIngredients newIngredient = new(KitchenId, ingredient.Id, ingredient.Quantity);

            bool addKitchenIngredientSuccess = await InteractionService.AddKitchenIngredientsAsync(newIngredient);
            if (addKitchenIngredientSuccess is false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to save {ingredient.Name} to your kitchen");
            }
        }

        async Task InsertIngredientRow()
        {
            insertingRow = true;
            disableAdd = true;
            var ingredient = new KitchenIngredientsViewModel();
            await ingredientsGrid.InsertRow(ingredient);
        }
        void CancelIngredientEdit(KitchenIngredientsViewModel ingredient)
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

        async Task EditIngredientRow(KitchenIngredientsViewModel ingredient)
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

        async Task SaveIngredientRow(KitchenIngredientsViewModel ingredient)
        {
            Ingredients currentIngredient = await InteractionService.RetrieveWarehouseIngredientByIdAsync(ingredient.Id);
            if (currentIngredient is null)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to retrieve {ingredient.Name}");
            }
            else
            {
                ingredient.Name = currentIngredient.Name;
                ingredient.UnitOfMeasurement = currentIngredient.UnitOfMeasurement;

                if (insertingRow == true)
                {
                    ingredientIds.Add(ingredient.Id);
                }
                disableAdd = false;

                await ingredientsGrid.UpdateRow(ingredient);
            }
        }

        async Task OnUpdateIngredientRow(KitchenIngredientsViewModel ingredient)
        {
            bool upsertKitchenIngredientsSuccess = await InteractionService.UpsertKitchenIngredientsAsync(KitchenId, ingredient);
            if (upsertKitchenIngredientsSuccess is false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to insert or update {ingredient.Name}");
            }
        }

        async Task DeleteIngredientRow(KitchenIngredientsViewModel ingredient)
        {
            var result = await DialogService.Confirm("Are you sure?", "Delete Row", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (result.HasValue && result.Value)
            {
                userIngredients.Remove(ingredient);
                ingredientIds.Remove(ingredient.Id);
                bool removeKitchenIngredientsSuccess = await InteractionService.RemoveKitchenIngredientsAsync(KitchenId, ingredient);
                if (removeKitchenIngredientsSuccess is false)
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to delete {ingredient.Name}");
                }
                await ingredientsGrid.Reload();
            }
            disableAdd = false;
        }
        async Task OnCreateRow(KitchenEquipmentViewModel equipment)
        {
            disableAdd = false;
            insertingRow = true;

            KitchenEquipment newEquipment = new(KitchenId, equipment.Id, equipment.Quantity);

            bool addKitchenEquipmentSuccess = await InteractionService.AddKitchenEquipmentAsync(newEquipment);
            if (addKitchenEquipmentSuccess is false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to save {equipment.Name} to your kitchen");
            }
        }

        async Task InsertEquipmentRow()
        {
            insertingRow = true;
            disableAdd = true;
            var equipment = new KitchenEquipmentViewModel();
            await equipmentGrid.InsertRow(equipment);
        }

        void CancelEquipmentEdit(KitchenEquipmentViewModel equipment)
        {
            if (currentlyEditingEquipment is not null)
            {
                equipment.Name = currentlyEditingEquipment.Name;
                equipment.Quantity = currentlyEditingEquipment.Quantity;
            }

            disableAdd = false;
            insertingRow = false;

            equipmentGrid.CancelEditRow(equipment);
        }

        async Task EditEquipmentRow(KitchenEquipmentViewModel equipment)
        {
            insertingRow = false;
            currentlyEditingEquipment = new()
            {
                Id = equipment.Id,
                Name = equipment.Name,
                Quantity = equipment.Quantity,
            };
            await equipmentGrid.EditRow(equipment);
        }

        async Task SaveEquipmentRow(KitchenEquipmentViewModel equipment)
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

                await equipmentGrid.UpdateRow(equipment);

                if (insertingRow == true)
                {
                    equipmentIds.Add(equipment.Id);
                }

                insertingRow = false;
            }
        }

        async Task OnUpdateRow(KitchenEquipmentViewModel equipment)
        {
            bool isSuccessful = await InteractionService.UpsertKitchenEquipmentAsync(KitchenId, equipment);
            if (isSuccessful == false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to save {equipment.Name} to your kitchen");
            }
        }

        async Task DeleteEquipmentRow(KitchenEquipmentViewModel equipment)
        {
            var result = await DialogService.Confirm("Are you sure?", "Delete Row", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (result.HasValue && result.Value)
            {
                userEquipment.Remove(equipment);
                equipmentIds.Remove(equipment.Id);
                bool removeKitchenEquipmentSuccessful = await InteractionService.RemoveKitchenEquipmentAsync(KitchenId, equipment);
                if (removeKitchenEquipmentSuccessful is false)
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to delete {equipment.Name} from your kitchen");
                }
                await equipmentGrid.Reload();
            }

        }
    }
}
