CREATE TRIGGER [publishing].[publishing_Snapshot_TR_UpdateChangeVersion] ON [publishing].[Snapshot] AFTER UPDATE AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [publishing].[Snapshot]
    SET ChangeVersion = (NEXT VALUE FOR [changes].[ChangeVersionSequence])
    FROM [publishing].[Snapshot] u
    WHERE EXISTS (SELECT 1 FROM inserted i WHERE i.id = u.id);
END	
GO

