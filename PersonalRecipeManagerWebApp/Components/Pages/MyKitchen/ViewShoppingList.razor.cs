using Microsoft.AspNetCore.Components;
using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models.Ingredients;
using PersonalRecipeManagerWebApp.Services;

namespace PersonalRecipeManagerWebApp.Components.Pages.MyKitchen
{
    public class ShoppingListViewModel
    {
        // ingredients = 1, equipment = 2
        public int ShoppingListType { get; set; }
        public Guid ItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Quantity { get; set; }

    }


    public partial class ViewShoppingList
    {
        [Inject] IHandleInteractionService InteractionService { get; set; }
        [SupplyParameterFromQuery] public Guid shoppingListId { get; set; }

        private IEnumerable<ShoppingListViewModel> items = new List<ShoppingListViewModel>();

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
