CREATE FUNCTION tracked_deletes_publishing.Snapshot_TR_DelTrkg()
    RETURNS trigger AS
$BODY$
BEGIN
    INSERT INTO tracked_deletes_publishing.Snapshot(SnapshotIdentifier, Id, ChangeVersion)
    VALUES (OLD.SnapshotIdentifier, OLD.Id, nextval('changes.ChangeVersionSequence'));
    RETURN NULL;
END;
$BODY$ LANGUAGE plpgsql;

CREATE TRIGGER TrackDeletes AFTER DELETE ON publishing.Snapshot 
    FOR EACH ROW EXECUTE PROCEDURE tracked_deletes_publishing.Snapshot_TR_DelTrkg();

