using Microsoft.AspNetCore.Components;
using PersonalRecipeManagerWebApp.Models;
using Radzen.Blazor;
using Radzen;
using PersonalRecipeManagerWebApp.Services;
using PersonalRecipeManagerWebApp.Models.Ingredients;

namespace PersonalRecipeManagerWebApp.Components.Pages.MyKitchen
{
    public partial class MyKitchens
    {
        [Inject] IHandleInteractionService InteractionService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] DialogService DialogService { get; set; }
        private User user { get; set; }
        [SupplyParameterFromQuery] private Guid UserId { get; set; }

        private IEnumerable<KitchenType> kTypes;

        private Guid selectedKitchen;


        RadzenDataGrid<Kitchen> kitchenGrid;
        private List<Kitchen> userKitchens;

        private bool disableAdd = false;
        bool disabled = true;

        private Kitchen currentlyEditingKitchen;

        protected override async Task OnInitializedAsync()
        {
            user = await InteractionService.RetrieveUserInformationByIdAsync(UserId);

            kTypes = await InteractionService.RetrieveAllKitchenTypesAsync();

            userKitchens = await InteractionService.RetrieveAllUserKitchensAsync(UserId);

            if (user is null)
            {
                NavigationManager.NavigateTo("notfound");
            }
        }

        void OnClickViewKitchen()
        {
            NavigationManager.NavigateTo($"/mykitchen/editkitchen?kitchenid={selectedKitchen}");
        }

        async Task OnCreateRow(Kitchen kitchen)
        {
            disableAdd = false;
            Guid newKitchenId = Guid.NewGuid();

            Kitchen newKitchen = new(newKitchenId, kitchen.TypeId, kitchen.Name);
            userKitchens.Add(newKitchen);

            bool addKitchenSuccessful = await InteractionService.AddKitchenAsync(newKitchen);
            if (addKitchenSuccessful is false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to add kitchen: {kitchen.Name})");
                return;
            }

            UserKitchen newUserKitchen = new()
            {
                UserId = UserId,
                KitchenId = newKitchenId
            };

            UserKitchen addUserKitchenSuccessful = await InteractionService.AddUserKitchenAsync(newUserKitchen);
            if (addUserKitchenSuccessful is null)
            {
                userKitchens.Remove(newKitchen);
                await InteractionService.RemoveKitchenAsync(newKitchen);
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to add kitchen: {kitchen.Name})");
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

        async Task OnEditKitchen(Kitchen kitchen)
        {
            currentlyEditingKitchen = new()
            {
                Id = kitchen.Id,
                Name = kitchen.Name,
            };

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

