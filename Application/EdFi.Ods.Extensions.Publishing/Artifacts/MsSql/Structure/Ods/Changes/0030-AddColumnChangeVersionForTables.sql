ALTER TABLE [publishing].[Snapshot] ADD [ChangeVersion] [BIGINT] DEFAULT (NEXT VALUE FOR [changes].[ChangeVersionSequence]) NOT NULL;

