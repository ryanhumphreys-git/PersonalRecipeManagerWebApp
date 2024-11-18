using Microsoft.AspNetCore.Components;
using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models.Ingredients;
using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Services;
using Radzen;
using PersonalRecipeManagerWebApp.Components.SharedDialog;

namespace PersonalRecipeManagerWebApp.Components.Pages.MyKitchen
{
    public partial class ViewShoppingList
    {
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] DialogService DialogService { get; set; }
        [Inject] IHandleInteractionService InteractionService { get; set; }
        [SupplyParameterFromQuery] public Guid shoppingListId { get; set; }
        [SupplyParameterFromQuery] public Guid UserId { get; set; }

        private List<ShoppingListViewModel> items = new List<ShoppingListViewModel>();

        private List<string> shoppingListItemNames = new();
        private List<ShoppingListViewModel> shoppingListViewModel = new();
        private List<ShoppingListIngredients> ingredients;
        private List<ShoppingListEquipment> equipments;

        private List<Guid> ingredientIds;
        private List<Guid> equipmentIds;

        private List<Ingredients> allIngredients;
        private List<Equipment> allEquipment;

        private List<Ingredients> currentIngredients;
        private List<Equipment> currentEquipment;


        private void OnSelectedItemsChanged(List<ShoppingListViewModel> selectedItems)
        {
            items = selectedItems;
        }
        async Task OnClickSubmitShoppingList()
        {
            Guid kitchenId = await DialogService.OpenAsync<KitchenSelectionDialogCard>("Select a kitchen to stock:",
                new Dictionary<string, object>(), 
                new DialogOptions() { Width = "400px", Height = "300px" });

            foreach(ShoppingListViewModel item in items)
            {
                if (item.ShoppingListType == 1)
                {
                    KitchenIngredients newKitchenIngredient = new(kitchenId, item.ItemId, item.Quantity);
                    bool addIngredient = await InteractionService.InsertKitchenIngredientsOrAddQuantityAsync(newKitchenIngredient);
                    if (addIngredient is false)
                    {
                        NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to save {item.Name} to your kitchen.");
                    }
                }
                if (item.ShoppingListType == 2)
                {
                    KitchenEquipmentViewModel newKitchenEquipment = new()
                    {
                        Id = item.ItemId,
                        Name = item.Name,
                        Quantity = item.Quantity
                    };
                    bool addEquipment = await InteractionService.UpsertKitchenEquipmentAsync(kitchenId, newKitchenEquipment);
                    if (addEquipment is false)
                    {
                        NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to save {item.Name} to your kitchen.");
                    }
                }
            }
        }


        protected override async Task OnInitializedAsync()
        {
            ingredients = await InteractionService.RetrieveShoppingListIngredientsByShoppingListIdAsync(shoppingListId);
            equipments = await InteractionService.RetrieveShoppingListEquipmentByShoppingListIdAsync(shoppingListId);

            ingredientIds = ingredients.Select(e => e.IngredientId).ToList();
            equipmentIds = equipments.Select(e => e.EquipmentId).ToList();

            allIngredients = await InteractionService.RetrieveAllWarehouseIngredientsAsync();
            allEquipment = await InteractionService.RetrieveAllWarehouseEquipmentAsync();

            currentIngredients = allIngredients.Where(e => ingredientIds.Contains(e.Id)).ToList();
            currentEquipment = allEquipment.Where(e => equipmentIds.Contains(e.Id)).ToList();

            foreach(var ingredient in currentIngredients)
            {
                shoppingListViewModel.Add(new()
                {
                    ShoppingListType = 1,
                    ItemId = ingredient.Id,
                    Name = ingredient.Name,
                    Quantity = ingredients.Where(i => i.IngredientId == ingredient.Id).First().Quantity,
                });
            }

            foreach (var equipment in currentEquipment)
            {
                shoppingListViewModel.Add(new()
                {
                    ShoppingListType = 2,
                    ItemId = equipment.Id,
                    Name = equipment.Name,
                    Quantity = equipments.Where(i => i.EquipmentId == equipment.Id).First().Quantity,
                });
            }
        }
    }
}
