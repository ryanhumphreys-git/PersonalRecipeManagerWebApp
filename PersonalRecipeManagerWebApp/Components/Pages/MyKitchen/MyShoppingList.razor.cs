using Microsoft.AspNetCore.Components;
using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.Ingredients;
using PersonalRecipeManagerWebApp.Services;
using Radzen;
using Radzen.Blazor;

namespace PersonalRecipeManagerWebApp.Components.Pages.MyKitchen
{
    public partial class MyShoppingList
    {
        private Guid kitchenId = new Guid("E7C14A98-BF68-4D93-A05E-EAD425347E9F");
        [Inject] IHandleInteractionService InteractionService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] DialogService DialogService { get; set; }
        private User user { get; set; }
        [SupplyParameterFromQuery] private Guid UserId { get; set; }

        private Guid selectedShoppingList;

        RadzenDataGrid<UserShoppingList> shoppingListGrid;
        private List<UserShoppingList> userShoppingLists;

        private UserShoppingList currentlyEditingList;

        private bool disableAdd = false;
        bool disabled = true;

        protected override async Task OnInitializedAsync()
        {
            user = await InteractionService.RetrieveUserInformationByIdAsync(UserId);

            userShoppingLists = await InteractionService.RetrieveAllUserShoppingListsAsync(UserId);

            if (user is null)
            {
                NavigationManager.NavigateTo("notfound");
            }

        }
        void OnClickViewShoppingList()
        {
            NavigationManager.NavigateTo($"mykitchen/editshoppinglist?shoppinglistid={selectedShoppingList}");
        }
        async Task OnCreateRow(UserShoppingList shoppingList)
        {
            disableAdd = false;

            UserShoppingList newShoppingList = new(UserId, Guid.NewGuid(), shoppingList.Name);
            userShoppingLists.Add(newShoppingList);

            bool addKitchenSuccessful = await InteractionService.AddUserShoppingListAsync(newShoppingList);
            if (addKitchenSuccessful is false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to save changes for {shoppingList.Name})");
            }
        }
        async Task InsertRow()
        {
            disableAdd = true;
            UserShoppingList shoppingList = new();
            await shoppingListGrid.InsertRow(shoppingList);
        }
        void CancelEdit(UserShoppingList shoppingList)
        {
            if (currentlyEditingList is not null)
            {
                shoppingList.Name = currentlyEditingList.Name;
            }

            disableAdd = false;
            shoppingListGrid.CancelEditRow(shoppingList);
        }
        async Task OnEditShoppingList (UserShoppingList shoppingList)
        {
            NavigationManager.NavigateTo($"/myrecipes/editrecipe?shoppingListid={shoppingList.ShoppingListId}&UserId={UserId}");
        }
        async Task SaveRow(UserShoppingList shoppingList)
        {
            disableAdd = false;

            await shoppingListGrid.UpdateRow(shoppingList);
        }
        async Task OnUpdateRow(UserShoppingList shoppingList)
        {
            UserShoppingList currentShoppingList = new(UserId, shoppingList.ShoppingListId, shoppingList.Name);
            bool upsertShoppingListSuccess = await InteractionService.UpsertUserShoppingListAsync(shoppingList);
            if (upsertShoppingListSuccess is false)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to insert or update {shoppingList.Name})");
            }
        }
        async Task DeleteRow(UserShoppingList shoppingList)
        {
            var result = await DialogService.Confirm("Are you sure?", "Delete Row", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (result.HasValue && result.Value)
            {
                userShoppingLists.Remove(shoppingList);
                bool removeKitchenSuccess = await InteractionService.RemoveUserShoppingListAsync(shoppingList);
                if (removeKitchenSuccess is false)
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Error", $"Unable to remove {shoppingList.Name})");
                }
                await shoppingListGrid.Reload();
            }
            disableAdd = false;
        }
    }
}
