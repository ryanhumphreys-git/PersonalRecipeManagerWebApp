using Microsoft.AspNetCore.Components;
using PersonalRecipeManagerWebApp.Models;
using Radzen;
using Radzen.Blazor;

namespace PersonalRecipeManagerWebApp.Components.Pages.MyInformation
{
    public partial class MyKitchenInfo
    {
        private Guid kitchenId = new Guid("E7C14A98-BF68-4D93-A05E-EAD425347E9F");
        bool disabled = true;

        [Inject] DialogService DialogService { get; set; }
        private User user { get; set; }
        [SupplyParameterFromQuery]
        private Guid Id { get; set; }

        private IEnumerable<KitchenType> kTypes;

        RadzenDataGrid<Kitchen> kitchenGrid;
        private List<Kitchen> userKitchens;

        private bool disableAdd = false;

        private Kitchen currentlyEditingKitchen;

        private void OnSwitchChange()
        {
            disabled = !disabled;
        }

        private async Task OnClick()
        {
            await InteractionService.UpdateUserInformationAsync(user);
        }

        protected override async Task OnInitializedAsync()
        {
            user = await InteractionService.RetrieveUserInformationByIdAsync(Id);

            kTypes = await InteractionService.RetrieveAllKitchenTypesAsync();

            userKitchens = await InteractionService.RetrieveAllUserKitchensAsync(Id);

            if (user is null)
            {
                NavigationManager.NavigateTo("notfound");
            }
        }

        async Task OnCreateRow(Kitchen kitchen)
        {
            disableAdd = false;

            Kitchen newKitchen = new(Id, kitchen.TypeId, kitchen.Name);

            await InteractionService.AddKitchenAsync(newKitchen);
        }

        async Task InsertRow()
        {
            disableAdd = true;
            Kitchen kitchen = new();
            await kitchenGrid.InsertRow(kitchen);
        }

        void CancelEdit(Kitchen kitchen)
        {
            if (currentlyEditingKitchen is not null)
            {
                kitchen.Name = currentlyEditingKitchen.Name;
            }

            disableAdd = false;
            kitchenGrid.CancelEditRow(kitchen);
        }

        async Task EditRow(Kitchen kitchen)
        {
            currentlyEditingKitchen = new(kitchen.Id, kitchen.TypeId, kitchen.Name);

            disableAdd = true;

            await kitchenGrid.EditRow(kitchen);
        }

        async Task SaveRow(Kitchen kitchen)
        {
            KitchenType kitchenType = await InteractionService.RetrieveKitchenTypeByTypeIdAsync(kitchen.TypeId);
            kitchen.TypeName = kitchenType.Name;

            disableAdd = false;

            await kitchenGrid.UpdateRow(kitchen);
        }

        async Task OnUpdateRow(Kitchen kitchen)
        {
            Kitchen currentKitchen = new(kitchen.Id, kitchen.TypeId, kitchen.Name);
            await InteractionService.UpsertKitchenAsync(kitchen);
        }

        async Task DeleteRow(Kitchen kitchen)
        {
            var result = await DialogService.Confirm("Are you sure?", "Delete Row", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (result.HasValue && result.Value)
            {
                userKitchens.Remove(kitchen);
                await InteractionService.RemoveKitchenAsync(kitchen);
                await kitchenGrid.Reload();
            }
            disableAdd = false;
        }
    }
}
