CREATE TABLE tracked_deletes_publishing.Snapshot
(
       SnapshotIdentifier VARCHAR(32) NOT NULL,
       Id UUID NOT NULL,
       ChangeVersion BIGINT NOT NULL,
       CONSTRAINT Snapshot_PK PRIMARY KEY (ChangeVersion)
);

