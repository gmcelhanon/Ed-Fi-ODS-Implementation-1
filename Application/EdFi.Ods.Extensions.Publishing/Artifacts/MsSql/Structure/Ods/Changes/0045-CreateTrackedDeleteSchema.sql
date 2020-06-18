IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'tracked_deletes_publishing')
EXEC sys.sp_executesql N'CREATE SCHEMA [tracked_deletes_publishing]'
GO