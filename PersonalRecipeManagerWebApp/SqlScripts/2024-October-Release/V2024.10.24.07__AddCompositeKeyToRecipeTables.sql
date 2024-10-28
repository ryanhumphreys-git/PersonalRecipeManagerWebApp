BEGIN TRANSACTION

SET NOCOUNT ON

BEGIN TRY


	PRINT CONCAT('Adding Kitchen Table And Updating Kitchen in Entity Table', GetDate())
	PRINT ''

	PRINT 'Remove Primary Keys Add Non Null Columns Add New Composite Key...'
        
    ALTER TABLE RecipeIngredients
	DROP CONSTRAINT PK__RecipeIn__6B232905C9FA5D74;

	ALTER TABLE RecipeIngredients
	DROP COLUMN AutoId;

	ALTER TABLE RecipeIngredients
	ALTER COLUMN RecipeId UNIQUEIDENTIFIER NOT NULL;

	ALTER TABLE RecipeIngredients
	ALTER COLUMN IngredientId UNIQUEIDENTIFIER NOT NULL;

	ALTER TABLE RecipeIngredients
	ADD CONSTRAINT PK_RecipeId_IngredientId PRIMARY KEY (RecipeId, IngredientId);

	ALTER TABLE RecipeEquipment
	DROP CONSTRAINT PK__RecipeTo__6B232905A760B805;

	ALTER TABLE RecipeEquipment
	DROP COLUMN AutoId;

	ALTER TABLE RecipeEquipment
	ALTER COLUMN RecipeId UNIQUEIDENTIFIER NOT NULL;

	ALTER TABLE RecipeEquipment
	ALTER COLUMN EquipmentId UNIQUEIDENTIFIER NOT NULL;

	ALTER TABLE RecipeEquipment
	ADD CONSTRAINT PK_RecipeId_EquipmentId PRIMARY KEY (RecipeId, EquipmentId);

	ALTER TABLE KitchenEquipment
	DROP CONSTRAINT PK__KitchenEquipment;

	ALTER TABLE KitchenEquipment
	DROP COLUMN AutoId;

	ALTER TABLE KitchenEquipment
	ALTER COLUMN KitchenId UNIQUEIDENTIFIER NOT NULL;

	ALTER TABLE KitchenEquipment
	ALTER COLUMN EquipmentId UNIQUEIDENTIFIER NOT NULL;

	ALTER TABLE KitchenEquipment
	ADD CONSTRAINT PK_KitchenId_EquipmentId PRIMARY KEY (KitchenId, EquipmentId);

	ALTER TABLE KitchenIngredients
	DROP CONSTRAINT PK__KitchenI__6B2329058B1AE66A;

	ALTER TABLE KitchenIngredients
	DROP COLUMN AutoId;

	ALTER TABLE KitchenIngredients
	ALTER COLUMN KitchenId UNIQUEIDENTIFIER NOT NULL;

	ALTER TABLE KitchenIngredients
	ALTER COLUMN IngredientId UNIQUEIDENTIFIER NOT NULL;

	ALTER TABLE KitchenIngredients
	ADD CONSTRAINT PK_KitchenId_IngredientId PRIMARY KEY (KitchenId, IngredientId);

    PRINT '.... DONE'
	PRINT ''

	COMMIT TRANSACTION

	PRINT CONCAT('Scripts Completed Successfully: ', GetDate())

END TRY
BEGIN CATCH

	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION

	PRINT ''
	PRINT CONCAT('An Error Occurred: ', GetDate()) 
	PRINT CONCAT(ERROR_NUMBER(),' - ', ERROR_MESSAGE())

END CATCH

SET NOCOUNT OFF




