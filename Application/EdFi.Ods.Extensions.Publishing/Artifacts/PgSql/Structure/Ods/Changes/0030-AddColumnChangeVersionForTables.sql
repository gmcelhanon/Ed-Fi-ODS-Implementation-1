ALTER TABLE publishing.Snapshot
ADD ChangeVersion BIGINT DEFAULT nextval('changes.ChangeVersionSequence') NOT NULL;

