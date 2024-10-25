BEGIN TRANSACTION

SET NOCOUNT ON

BEGIN TRY


	PRINT CONCAT('Adding Kitchen Table And Updating Kitchen in Entity Table', GetDate())
	PRINT ''

	PRINT 'Adding Entity Recipes...'
        
    CREATE TABLE EntityRecipes
	(
		AutoId UNIQUEIDENTIFIER NOT NULL,
		EntityId UNIQUEIDENTIFIER,
		RecipeId UNIQUEIDENTIFIER,
		PRIMARY KEY(AutoId),
		CONSTRAINT FK_EntityRecipes_Entity FOREIGN KEY (EntityId) REFERENCES Entity (Id),
		CONSTRAINT FK_EntityRecipes_Recipes FOREIGN KEY (RecipeId) REFERENCES Recipes (Id)
	);

	PRINT 'Adding Entity Kitchens...'

	CREATE TABLE EntityKitchens
	(
		AutoId UNIQUEIDENTIFIER NOT NULL,
		EntityId UNIQUEIDENTIFIER,
		KitchenId UNIQUEIDENTIFIER,
		PRIMARY KEY(AutoId),
		CONSTRAINT FK_EntityKitchens_Entity FOREIGN KEY (EntityId) REFERENCES Entity (Id),
		CONSTRAINT FK_EntityKitchens_Kitchen FOREIGN KEY (KitchenId) REFERENCES Kitchen (Id)
	);

	PRINT 'Drop KitchenId From Entity Table...'

	ALTER TABLE Entity
	DROP COLUMN KitchenId

	PRINT 'Rename Entity To User...'

	EXEC sp_rename 'Entity', 'User'
	EXEC sp_rename 'EntityKitchens.EntityId', 'UserId'
	EXEC sp_rename 'EntityKitchens', 'UserKitchens'
	EXEC sp_rename 'EntityRecipes.EntityId', 'UserId'
	EXEC sp_rename 'EntityRecipes', 'UserRecipes'
	

	PRINT 'Populating New Tables...'

	DECLARE @user UNIQUEIDENTIFIER
	SELECT @user = u.Id
	FROM [User] u
	WHERE u.Name = 'Ryan'
	DECLARE @cheeseburger UNIQUEIDENTIFIER
	SELECT @cheeseburger = r.Id
	FROM Recipes r
	WHERE r.Name = 'cheeseburger'
	DECLARE @pennealfredo UNIQUEIDENTIFIER
	SELECT @pennealfredo = r.Id
	FROM Recipes r
	WHERE r.Name = 'penne alfredo'
	DECLARE @porkburger UNIQUEIDENTIFIER
	SELECT @porkburger = r.Id
	FROM Recipes r
	WHERE r.Name = 'pork burger'

	INSERT INTO UserRecipes
	VALUES
	(NEWID(), @user , @cheeseburger),
	(NEWID(), @user , @pennealfredo),
	(NEWID(), @user , @porkburger)

	DECLARE @userKitchen UNIQUEIDENTIFIER
	SELECT @userKitchen = k.Id
	FROM Kitchen k
	INSERT INTO UserKitchens
	VALUES
	(NEWID(), @user , @userKitchen)

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




