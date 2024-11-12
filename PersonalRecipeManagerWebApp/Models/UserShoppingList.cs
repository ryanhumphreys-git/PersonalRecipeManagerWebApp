using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models.Ingredients;

namespace PersonalRecipeManagerWebApp.Models
{
    public class UserShoppingList
    {
        public Guid UserId { get; set; }
        public Guid ShoppingListId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ShoppingListEquipment> ShoppingListEquipments { get; set; } = new List<ShoppingListEquipment>();
        public virtual ICollection<ShoppingListIngredients> ShoppingListIngredients { get; set; } = new List<ShoppingListIngredients>();
        public virtual User? User { get; set; }

        public UserShoppingList() { }

        public UserShoppingList(Guid userId, Guid shoppingListId, string name)
        {
            UserId = userId;
            ShoppingListId = shoppingListId;
            Name = name;
        }
    }
}
