using Microsoft.AspNetCore.Components;
using PersonalRecipeManagerWebApp.Models;
using Radzen;
using Radzen.Blazor;

namespace PersonalRecipeManagerWebApp.Components.Pages.MyKitchen
{
    public partial class EquipmentInventory
    {
        private Guid kitchenId = new Guid("E7C14A98-BF68-4D93-A05E-EAD425347E9F");
        [SupplyParameterFromQuery]
        private Guid Id { get; set; }

        [Inject] DialogService DialogService { get; set; }
        RadzenDataGrid<KitchenEquipmentViewModel> equipmentGrid;
        List<KitchenEquipmentViewModel> userEquipment;
        List<Equipment> allEquipment;
        List<Guid> equipmentIds;
        private User user { get; set; }

        private KitchenEquipmentViewModel currentlyEditingEquipment;

        private bool insertingRow = false;
        private bool disableAdd;

        private List<Equipment> FilteredEquipmentList =>
            allEquipment.Where(e => !equipmentIds.Contains(e.Id)).ToList();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            user = await InteractionService.RetrieveUserInformationByIdAsync(Id);

            userEquipment = await InteractionService.RetrieveKitchenEquipmentDtoByKitchenIdAsync(kitchenId);
            equipmentIds = userEquipment.Select(e => e.Id).ToList(); 

            allEquipment = await InteractionService.RetrieveAllWarehouseEquipmentAsync();
        }

        async Task OnCreateRow(KitchenEquipmentViewModel equipment)
        {
            disableAdd = false;
            insertingRow = true;

            KitchenEquipment newEquipment = new(kitchenId, equipment.Id, equipment.Quantity);

            await InteractionService.AddKitchenEquipmentAsync(newEquipment);
        }

        async Task InsertRow()
        {
            insertingRow = true;
            disableAdd = true;
            var equipment = new KitchenEquipmentViewModel();
            await equipmentGrid.InsertRow(equipment);
        }

        void CancelEdit(KitchenEquipmentViewModel equipment)
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

        async Task EditRow(KitchenEquipmentViewModel equipment)
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

        async Task SaveRow(KitchenEquipmentViewModel equipment)
        {
            Equipment currentEquipment = await InteractionService.RetrieveWarehouseEquipmentByIdAsync(equipment.Id);
            equipment.Name = currentEquipment.Name;
            disableAdd = false;

            await equipmentGrid.UpdateRow(equipment);
            
            if (insertingRow == true)
            {
                equipmentIds.Add(equipment.Id);
            }

            insertingRow = false;
        }

        async Task OnUpdateRow(KitchenEquipmentViewModel equipment)
        {
            await InteractionService.UpsertKitchenEquipmentAsync(kitchenId, equipment);
        }

        async Task DeleteRow(KitchenEquipmentViewModel equipments)
        {
            var result = await DialogService.Confirm("Are you sure?", "Delete Row", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (result.HasValue && result.Value)
            {
                userEquipment.Remove(equipments);
                equipmentIds.Remove(equipments.Id);
                await InteractionService.RemoveKitchenEquipmentAsync(kitchenId, equipments);
                await equipmentGrid.Reload();
            }
            
        }
    }
}
