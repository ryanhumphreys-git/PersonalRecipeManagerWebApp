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

        [Inject] NotificationService NotificationService { get; set; }
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
            bool updateUserInformationSuccess = await InteractionService.UpdateUserInformationAsync(user);
            if (updateUserInformationSuccess is false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to save changes for {user.Name})");
            }
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

            bool addKitchenSuccessful = await InteractionService.AddKitchenAsync(newKitchen);
            if (addKitchenSuccessful is false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to save changes for {kitchen.Name})");
            }
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
            if (kitchenType is null)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to retrieve {kitchen.Name})");
            }
            else
            {
                kitchen.TypeName = kitchenType.Name;

                disableAdd = false;

                await kitchenGrid.UpdateRow(kitchen);
            }
        }

        async Task OnUpdateRow(Kitchen kitchen)
        {
            Kitchen currentKitchen = new(kitchen.Id, kitchen.TypeId, kitchen.Name);
            bool upsertKitchenSuccess = await InteractionService.UpsertKitchenAsync(kitchen);
            if (upsertKitchenSuccess is false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to insert or update {kitchen.Name})");
            }
        }

        async Task DeleteRow(Kitchen kitchen)
        {
            var result = await DialogService.Confirm("Are you sure?", "Delete Row", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (result.HasValue && result.Value)
            {
                userKitchens.Remove(kitchen);
                bool removeKitchenSuccess = await InteractionService.RemoveKitchenAsync(kitchen);
                if (removeKitchenSuccess is false)
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to remove {kitchen.Name})");
                }
                await kitchenGrid.Reload();
            }
            disableAdd = false;
        }
    }
}
