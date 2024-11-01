﻿using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask AddKitchenIngredientsAsync(KitchenIngredients ingredients);
        public ValueTask<List<KitchenIngredientsViewModel>> RetrieveKitchenIngredientsDtoByKitchenIdAsync(Guid id);
        public ValueTask<KitchenIngredients> RetrieveKitchenIngredientByIdAsync(Guid kitchenId, Guid ingredientId);
        public ValueTask UpsertKitchenIngredientsAsync(Guid kitchenId, KitchenIngredientsViewModel ingredients);
        public ValueTask RemoveKitchenIngredientsAsync(Guid kitchenId, KitchenIngredientsViewModel ingredients);
        public ValueTask<bool> CheckIfKitchenHasIngredientByIdAsync(Guid kitchenId, Guid ingredientId);

    }
}
