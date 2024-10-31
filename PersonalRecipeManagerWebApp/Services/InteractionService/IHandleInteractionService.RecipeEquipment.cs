﻿using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial interface IHandleInteractionService
    {
        public ValueTask AddRecipeEquipmentAsync(RecipeEquipment equipment);
        public ValueTask<List<RecipeEquipmentViewModel>> RetrieveRecipeEquipmentDtoByRecipeIdAsync(Guid id);
        public ValueTask<RecipeEquipment> RetrieveRecipeEquipmentByIdAsync(Guid recipeId, Guid equipmentId);
        public ValueTask UpsertRecipeEquipmentAsync(Guid recipeId, RecipeEquipmentViewModel equipment);
        public ValueTask RemoveRecipeEquipmentAsync(Guid recipeId, RecipeEquipmentViewModel equipment);
        public ValueTask<bool> CheckIfRecipeHasEquipmentByIdAsync(Guid recipeId, Guid equipmentId);

    }
}