BEGIN TRANSACTION

SET NOCOUNT ON

BEGIN TRY


	PRINT CONCAT('Adding seperate tables for unique items and initializing with current default item data', GetDate())
	PRINT ''

	PRINT 'Creating UserShoppingList Table...'
        
    CREATE TABLE UserShoppingList
    (
        UserId UNIQUEIDENTIFIER NOT NULL,
        ShoppingListId UNIQUEIDENTIFIER NOT NULL,
        Name NVARCHAR(50),
        CONSTRAINT PK_UserShoppingList PRIMARY KEY (ShoppingListId),
        CONSTRAINT FK_UserShoppingList_User FOREIGN KEY (UserId) REFERENCES [User] (Id)
    );

    PRINT 'Creating ShoppingListIngredients Table...'

    CREATE TABLE ShoppingListIngredients
    (
        ShoppingListId UNIQUEIDENTIFIER NOT NULL,
        IngredientId UNIQUEIDENTIFIER NOT NULL,
        Quantity DECIMAL,
        CONSTRAINT PK_ShoppingListIngredients PRIMARY KEY (ShoppingListId, IngredientId),
        CONSTRAINT FK_ShoppingListIngredients_UserShoppingList FOREIGN KEY (ShoppingListId) REFERENCES UserShoppingList (ShoppingListId),
        CONSTRAINT FK_ShoppingListIngredients_Ingredients FOREIGN KEY (IngredientId) REFERENCES Ingredients (Id)
    );

    PRINT 'Creating ShoppingListEquipment Table...'

    CREATE TABLE ShoppingListEquipment
    (
        ShoppingListId UNIQUEIDENTIFIER NOT NULL,
        EquipmentId UNIQUEIDENTIFIER NOT NULL,
        Quantity DECIMAL,
        CONSTRAINT PK_ShoppingListEquipment PRIMARY KEY (ShoppingListId, EquipmentId),
        CONSTRAINT FK_ShoppingListEquipment_UserShoppingList FOREIGN KEY (ShoppingListId) REFERENCES UserShoppingList (ShoppingListId),
        CONSTRAINT FK_ShoppingListEquipment_Equipment FOREIGN KEY (EquipmentId) REFERENCES Equipment (Id)
    );
    

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






