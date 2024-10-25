BEGIN TRANSACTION

SET NOCOUNT ON

BEGIN TRY


	PRINT CONCAT('Adding Kitchen Table And Updating Kitchen in Entity Table', GetDate())
	PRINT ''

	PRINT 'Adding Kitchen Table...'
        
    CREATE TABLE Kitchen
	(
		Id UNIQUEIDENTIFIER NOT NULL,
		TypeId UNIQUEIDENTIFIER,
		PRIMARY KEY(Id),
		CONSTRAINT FK_Kitchen_KitchenType FOREIGN KEY (TypeId) REFERENCES KitchenType(Id)
	);

	PRINT 'Inserting Kitchen Table Value...'

	DECLARE @bareId UNIQUEIDENTIFIER
	SELECT @bareId = kt.Id
	FROM KitchenType kt
	WHERE kt.Name = 'bare'
	INSERT INTO Kitchen
	VALUES
	(NEWID(), @bareId);

	PRINT 'Updating Kitchen Column In Entity...'

	EXEC sp_rename 'Entity.KitchenTypeId', 'KitchenId';

	PRINT 'Updating Kitchen Column Value In Entity...'

	UPDATE Entity
	SET KitchenId = (SELECT k.Id
					FROM Kitchen k
					JOIN KitchenType kt ON kt.Id = k.TypeId 
					WHERE kt.Name = 'bare')
	WHERE Entity.Name = 'Ryan';

	PRINT 'Drop FK Relationships From KitchenIngredients and KitchenEquipment From KitchenType...'

	ALTER TABLE KitchenIngredients
	DROP CONSTRAINT FK_KitchenIngredients_KitchenType;

	ALTER TABLE KitchenEquipment
	DROP CONSTRAINT FK_KitchenToolsAndEquipment_KitchenType;

	PRINT 'Update KitchenIngredients and KitchenEquipment KitchenId Column...'

	UPDATE KitchenEquipment
	SET KitchenId = (SELECT e.KitchenId
					FROM Entity e);

	UPDATE KitchenIngredients
	SET KitchenId = (SELECT e.KitchenId
					FROM Entity e);

	PRINT 'Add FK Relationships with KitchenIngredients, KitchenEquipment, Kitchen, And Entity...'

	ALTER TABLE Entity
	WITH CHECK ADD CONSTRAINT FK_Entity_Kitchen
	FOREIGN KEY (KitchenId) REFERENCES Kitchen (Id)

	ALTER TABLE KitchenIngredients
	WITH CHECK ADD CONSTRAINT FK_KitchenIngredients_Kitchen
	FOREIGN KEY(KitchenId) REFERENCES Kitchen (Id)

	ALTER TABLE KitchenIngredients CHECK CONSTRAINT FK_KitchenIngredients_Kitchen

	ALTER TABLE KitchenEquipment
	WITH CHECK ADD CONSTRAINT FK_KitchenEquipment_Kitchen
	FOREIGN KEY(KitchenId) REFERENCES Kitchen (Id)

	ALTER TABLE KitchenEquipment CHECK CONSTRAINT FK_KitchenEquipment_Kitchen

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




