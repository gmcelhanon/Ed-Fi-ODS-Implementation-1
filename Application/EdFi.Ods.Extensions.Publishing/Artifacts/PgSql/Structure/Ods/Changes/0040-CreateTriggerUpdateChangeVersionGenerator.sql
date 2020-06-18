CREATE TRIGGER UpdateChangeVersion BEFORE UPDATE ON publishing.Snapshot
    FOR EACH ROW EXECUTE PROCEDURE changes.UpdateChangeVersion();

