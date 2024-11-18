using Microsoft.AspNetCore.Components;
using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models.Ingredients;
using PersonalRecipeManagerWebApp.Services;
using Radzen;
using Radzen.Blazor;

namespace PersonalRecipeManagerWebApp.Components.Pages.MyKitchen
{
    public partial class EditShoppingList
    {
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] DialogService DialogService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IHandleInteractionService InteractionService { get; set; }
        [SupplyParameterFromQuery] public Guid ShoppingListId { get; set; }
        [SupplyParameterFromQuery] public Guid UserId { get; set; }

        RadzenDataGrid<ShoppingListEquipment> equipmentGrid;
        List<ShoppingListEquipment> shoppingListEquipment;
        List<Equipment> allEquipment;
        List<Guid> equipmentIds;

        RadzenDataGrid<ShoppingListIngredients> ingredientsGrid;
        List<ShoppingListIngredients> shoppingListIngredients;
        List<Ingredients> allIngredients;
        List<Guid> ingredientIds;

        private ShoppingListEquipment currentlyEditingEquipment;
        private ShoppingListIngredients currentlyEditingIngredient;

        private UserShoppingList selectedShoppingList;
        private User user { get; set; }

        private bool isLoading = true;
        private bool disableAdd;
        private bool insertingRow;

        private bool shoppingListIngredientsFound = true;
        private bool shoppingListEquipmentFound = true;
        private bool allIngredientsFound = true;
        private bool allEquipmentFound = true;

        private List<Ingredients> FilteredIngredientList =>
            allIngredients.Where(e => !ingredientIds.Contains(e.Id)).ToList();
        private List<Equipment> FilteredEquipmentList =>
            allEquipment.Where(e => !equipmentIds.Contains(e.Id)).ToList();

        private List<Ingredients> ingredientNames =>
            allIngredients.Where(e => ingredientIds.Contains(e.Id)).ToList();
        private List<Equipment> equipmentNames =>
            allEquipment.Where(e => equipmentIds.Contains(e.Id)).ToList();
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            selectedShoppingList = await InteractionService.RetrieveShoppingListByShoppingListIdAsync(ShoppingListId);
            shoppingListIngredients = await InteractionService.RetrieveShoppingListIngredientsByShoppingListIdAsync(ShoppingListId);
            if (shoppingListIngredients is null)
            {
                shoppingListIngredientsFound = false;
            }
            else
            {
                ingredientIds = shoppingListIngredients.Select(e => e.IngredientId).ToList();
            }

            allIngredients = await InteractionService.RetrieveAllWarehouseIngredientsAsync();
            if (allIngredients is null)
            {
                allIngredientsFound = false;
            }

            shoppingListEquipment = await InteractionService.RetrieveShoppingListEquipmentByShoppingListIdAsync(ShoppingListId);
            if (shoppingListEquipment is null)
            {
                shoppingListEquipmentFound = false;
            }
            else
            {
                equipmentIds = shoppingListEquipment.Select(e => e.EquipmentId).ToList();
            }

            allEquipment = await InteractionService.RetrieveAllWarehouseEquipmentAsync();
            if (allEquipment is null)
            {
                allEquipmentFound = false;
            }

            isLoading = false;
        }

        void OnClickViewShoppingList()
        {
            NavigationManager.NavigateTo($"/mykitchen/myshoppinglist/view?shoppinglistid={ShoppingListId}&userid={UserId}");
        }

        void OnIngredientSelectionChange(object value, ShoppingListIngredients ingredient)
        {
            if (value is Guid selectedId)
            {
                var selectedIngredient = FilteredIngredientList.FirstOrDefault(i => i.Id == selectedId);
                if (selectedIngredient != null)
                {
                    StateHasChanged();
                }
            }
        }

        async Task OnCreateIngredientRow(ShoppingListIngredients ingredient)
        {
            disableAdd = false;
            insertingRow = true;

            ShoppingListIngredients newIngredient = new(ShoppingListId, ingredient.IngredientId, ingredient.Quantity);

            bool addKitchenIngredientSuccess = await InteractionService.AddShoppingListIngredientsAsync(newIngredient);
            if (addKitchenIngredientSuccess is false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to save ingredient to your kitchen");
            }
        }

        async Task InsertIngredientRow()
        {
            insertingRow = true;
            disableAdd = true;
            var ingredient = new ShoppingListIngredients();
            await ingredientsGrid.InsertRow(ingredient);
        }

        async Task SaveIngredientRow(ShoppingListIngredients ingredient)
        {
            Ingredients currentIngredient = await InteractionService.RetrieveWarehouseIngredientByIdAsync(ingredient.IngredientId);
            if (currentIngredient is null)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to retrieve ingredient");
            }
            else
            {
                if (insertingRow == true)
                {
                    ingredientIds.Add(ingredient.IngredientId);
                }
                disableAdd = false;

                await ingredientsGrid.UpdateRow(ingredient);
            }
        }

        void CancelIngredientEdit(ShoppingListIngredients ingredient)
        {
            disableAdd = false;
            ingredientsGrid.CancelEditRow(ingredient);
        }

        async Task DeleteIngredientRow(ShoppingListIngredients ingredient)
        {
            var result = await DialogService.Confirm("Are you sure?", "Delete Row", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (result.HasValue && result.Value)
            {
                shoppingListIngredients.Remove(ingredient);
                ingredientIds.Remove(ingredient.IngredientId);
                bool removeKitchenIngredientsSuccess = await InteractionService.RemoveShoppingListIngredientAsync(ingredient);
                if (removeKitchenIngredientsSuccess is false)
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to delete ingredient");
                }
                await ingredientsGrid.Reload();
            }
            disableAdd = false;
        }
        async Task OnCreateEquipmentRow(ShoppingListEquipment equipment)
        {
            disableAdd = false;
            insertingRow = true;

            ShoppingListEquipment newEquipment = new(ShoppingListId, equipment.EquipmentId, equipment.Quantity);

            bool addKitchenEquipmentSuccess = await InteractionService.AddShoppingListEquipmentAsync(newEquipment);
            if (addKitchenEquipmentSuccess is false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to save equipment to your kitchen");
            }
        }

        async Task InsertEquipmentRow()
        {
            insertingRow = true;
            disableAdd = true;
            var equipment = new ShoppingListEquipment();
            await equipmentGrid.InsertRow(equipment);
        }

        async Task SaveEquipmentRow(ShoppingListEquipment equipment)
        {
            Equipment currentEquipment = await InteractionService.RetrieveWarehouseEquipmentByIdAsync(equipment.EquipmentId);
            if (currentEquipment is null)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to retrieve equipment");
            }
            else
            {
                disableAdd = false;

                await equipmentGrid.UpdateRow(equipment);

                if (insertingRow == true)
                {
                    equipmentIds.Add(equipment.EquipmentId);
                }

                insertingRow = false;
            }
        }
        void CancelEquipmentEdit(ShoppingListEquipment equipment)
        {
            disableAdd = false;
            equipmentGrid.CancelEditRow(equipment);
        }

        async Task DeleteEquipmentRow(ShoppingListEquipment equipment)
        {
            var result = await DialogService.Confirm("Are you sure?", "Delete Row", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (result.HasValue && result.Value)
            {
                shoppingListEquipment.Remove(equipment);
                equipmentIds.Remove(equipment.EquipmentId);
                bool removeKitchenEquipmentSuccessful = await InteractionService.RemoveShoppingListEquipmentAsync(equipment);
                if (removeKitchenEquipmentSuccessful is false)
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to delete equipment from your kitchen");
                }
                await equipmentGrid.Reload();
            }

        }
    }
}

