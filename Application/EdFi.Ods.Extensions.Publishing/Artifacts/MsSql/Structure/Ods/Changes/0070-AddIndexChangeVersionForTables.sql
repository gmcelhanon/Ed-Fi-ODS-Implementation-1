BEGIN TRANSACTION
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'publishing.Snapshot') AND name = N'UX_Snapshot_ChangeVersion')
    CREATE INDEX [UX_Snapshot_ChangeVersion] ON [publishing].[Snapshot] ([ChangeVersion] ASC)
    GO
COMMIT

