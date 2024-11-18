using Microsoft.AspNetCore.Components;

namespace PersonalRecipeManagerWebApp.Components.Pages.MyKitchen
{
    public partial class AddShoppingListCard
    {
        private string shoppingListName;

        void OnSubmit()
        {
            dialogService.Close(shoppingListName);
        }

    }
}
