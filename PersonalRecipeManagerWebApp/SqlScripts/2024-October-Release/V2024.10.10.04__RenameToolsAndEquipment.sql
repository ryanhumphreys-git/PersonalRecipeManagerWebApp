BEGIN TRANSACTION

SET NOCOUNT ON

BEGIN TRY


	PRINT CONCAT('Adding seperate tables for unique items and initializing with current default item data', GetDate())
	PRINT ''

	PRINT 'Renaming All Instances Of ToolsAndEquipment To Equipment...'
        
    EXEC sp_rename 'KitchenToolsAndEquipment.ToolsAndEquipmentId', 'EquipmentId', 'COLUMN'
    EXEC sp_rename 'RecipeToolsAndEquipment.ToolsAndEquipmentId', 'EquipmentId', 'COLUMN'
    EXEC sp_rename 'ToolsAndEquipment', 'Equipment'
    EXEC sp_rename 'RecipeToolsAndEquipment', 'RecipeEquipment'
    EXEC sp_rename 'KitchenToolsAndEquipment', 'KitchenEquipment'

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






