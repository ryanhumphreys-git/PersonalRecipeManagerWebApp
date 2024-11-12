namespace PersonalRecipeManagerWebApp.Models.Ingredients
{
    public class ShoppingListIngredients
    {
        public Guid ShoppingListId { get; set; }
        public Guid IngredientId { get; set; }
        public double Quantity { get; set; }
        public virtual Ingredients? Ingredients { get; set; }
        public virtual UserShoppingList? UserShoppingList { get; set; }

        public ShoppingListIngredients() { }

        public ShoppingListIngredients(Guid shoppingListId, Guid ingredientId, double quantity)
        {
            ShoppingListId = shoppingListId;
            IngredientId = ingredientId;
            Quantity = quantity;
        }
    }
}
