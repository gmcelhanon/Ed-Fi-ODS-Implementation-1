CREATE TABLE [tracked_deletes_publishing].[Snapshot]
(
       SnapshotIdentifier [NVARCHAR](32) NOT NULL,
       Id uniqueidentifier NOT NULL,
       ChangeVersion bigint NOT NULL,
       CONSTRAINT PK_Snapshot PRIMARY KEY CLUSTERED (ChangeVersion)
)

