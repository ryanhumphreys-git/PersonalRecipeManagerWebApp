using Microsoft.AspNetCore.Components;
using PersonalRecipeManagerWebApp.Models.Equipment;
using Radzen;
using Radzen.Blazor;

namespace PersonalRecipeManagerWebApp.Components.Pages.ManageWarehouse
{
    public partial class EquipmentWarehouse
    {
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] DialogService dialogService {  get; set; }
        RadzenDataGrid<Equipment> equipmentGrid;
        List<Equipment> allEquipment;

        private Equipment currentlyEditingEquipment;

        private bool disableAdd;
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            allEquipment = await InteractionService.RetrieveAllWarehouseEquipmentAsync();
        }

        async Task OnCreateRow(Equipment equipment)
        {
            disableAdd = false;
            bool addWarehouseEquipmentSuccess = await InteractionService.AddWarehouseEquipmentAsync(equipment);
            if (addWarehouseEquipmentSuccess is false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to insert {equipment.Name})");
            }
        }

        async Task InsertRow()
        {
            disableAdd = true;
            var equipment = new Equipment(Guid.NewGuid());
            await equipmentGrid.InsertRow(equipment);
        }
        void CancelEdit(Equipment equipment)
        {
            if (currentlyEditingEquipment is not null)
            {
                equipment.Name = currentlyEditingEquipment.Name;
                equipment.Cost = currentlyEditingEquipment.Cost;
            }

            disableAdd = false;

            equipmentGrid.CancelEditRow(equipment);
        }
        async Task EditRow(Equipment equipment)
        {
            currentlyEditingEquipment = new(equipment.Id, equipment.Name, equipment.Cost);

            disableAdd = true;

            await equipmentGrid.EditRow(equipment);
        }

        async Task SaveRow(Equipment equipment)
        {
            disableAdd = false;

            await equipmentGrid.UpdateRow(equipment);
        }

        async Task OnUpdateRow(Equipment equipment)
        {
            bool upsertWarehouseEquipmentSuccess = await InteractionService.UpsertWarehouseEquipmentAsync(equipment);
            if (upsertWarehouseEquipmentSuccess is false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to insert or update {equipment.Name})");
            }
        }

        async Task DeleteRow(Equipment equipment)
        {
            var result = await DialogService.Confirm("Are you sure?", "Delete Row", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (result.HasValue && result.Value)
            {
                allEquipment.Remove(equipment);
                bool removeWarehouseEquipmentSuccess = await InteractionService.RemoveWarehouseEquipmentAsync(equipment);
                if (removeWarehouseEquipmentSuccess is false)
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to remove {equipment.Name})");
                }
                await equipmentGrid.Reload();
            }
            disableAdd = false;
        }
    }
}
