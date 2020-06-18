CREATE TRIGGER [publishing].[publishing_Snapshot_TR_DeleteTracking] ON [publishing].[Snapshot] AFTER DELETE AS
BEGIN
    IF @@rowcount = 0 
        RETURN

    SET NOCOUNT ON

    INSERT INTO [tracked_deletes_publishing].[Snapshot](SnapshotIdentifier, Id, ChangeVersion)
    SELECT  SnapshotIdentifier, Id, (NEXT VALUE FOR [changes].[ChangeVersionSequence])
    FROM    deleted d
END
GO

ALTER TABLE [publishing].[Snapshot] ENABLE TRIGGER [publishing_Snapshot_TR_DeleteTracking]
GO


