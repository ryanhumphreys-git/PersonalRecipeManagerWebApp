BEGIN TRANSACTION

SET NOCOUNT ON

BEGIN TRY


	PRINT CONCAT('Adding seperate tables for unique items and initializing with current default item data', GetDate())
	PRINT ''

	PRINT 'Adding Instructions Variable To Recipes Table...'
        
	ALTER TABLE Recipes
	ADD Instructions NVARCHAR(MAX);

	PRINT 'Changing User Kitchens Primary Key To Composite...'

	ALTER TABLE UserKitchens
	ALTER COLUMN KitchenId UNIQUEIDENTIFIER NOT NULL

	ALTER TABLE UserKitchens
	ALTER COLUMN UserId UNIQUEIDENTIFIER NOT NULL

	ALTER TABLE UserKitchens
	DROP CONSTRAINT PK__EntityKi__6B232905495BDECC

	ALTER TABLE UserKitchens
	DROP COLUMN AutoId

	ALTER TABLE UserKitchens
	ADD CONSTRAINT PK_UserKitchens PRIMARY KEY (UserId, KitchenId)
   
    

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






