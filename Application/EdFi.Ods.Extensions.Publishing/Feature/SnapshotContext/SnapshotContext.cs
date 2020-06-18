namespace EdFi.Ods.Extensions.Publishing.Feature.SnapshotContext
{
    public class SnapshotContext
    {
        public SnapshotContext(string snapshotIdentifer)
        {
            SnapshotIdentifer = snapshotIdentifer;
        }

        public string SnapshotIdentifer { get; }

        public static SnapshotContext Empty { get; } = new SnapshotContext(null);
    }
}
