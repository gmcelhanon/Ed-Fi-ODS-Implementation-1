namespace EdFi.Ods.Extensions.Publishing.Feature.SnapshotContext
{
    public class SnapshotContext
    {
        public SnapshotContext(string snapshotIdentifier)
        {
            SnapshotIdentifier = snapshotIdentifier;
        }

        public string SnapshotIdentifier { get; }
    }
}
