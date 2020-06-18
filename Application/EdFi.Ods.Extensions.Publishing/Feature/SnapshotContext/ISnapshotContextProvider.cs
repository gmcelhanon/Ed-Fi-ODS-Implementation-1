namespace EdFi.Ods.Extensions.Publishing.Feature.SnapshotContext
{
    public interface ISnapshotContextProvider
    {
        SnapshotContext GetSnapshotContext();

        void SetSnapshotContext(SnapshotContext apiKeyContext);
    }
}
