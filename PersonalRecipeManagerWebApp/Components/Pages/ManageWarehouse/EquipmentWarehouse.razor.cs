using Microsoft.AspNetCore.Components;
using PersonalRecipeManagerWebApp.Models;
using Radzen;
using Radzen.Blazor;

namespace PersonalRecipeManagerWebApp.Components.Pages.ManageWarehouse
{
    public partial class EquipmentWarehouse
    {
        [Inject] DialogService dialogService {  get; set; }
        RadzenDataGrid<Equipment> equipmentGrid;
        List<Equipment> equipment;

        private Equipment currentlyEditingEquipment;

        private bool disableAdd;
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            equipment = await InteractionService.RetrieveAllWarehouseEquipmentAsync();
        }

        async Task OnCreateRow(Equipment equipment)
        {
            disableAdd = false;
            await InteractionService.AddWarehouseEquipmentAsync(equipment);
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
            await InteractionService.UpsertWarehouseEquipmentAsync(equipment);
        }

        async Task DeleteRow(Equipment equipments)
        {
            var result = await DialogService.Confirm("Are you sure?", "Delete Row", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (result.HasValue && result.Value)
            {
                equipment.Remove(equipments);
                await InteractionService.RemoveWarehouseEquipmentAsync(equipments);
                await equipmentGrid.Reload();
            }
            disableAdd = false;
        }
    }
}
