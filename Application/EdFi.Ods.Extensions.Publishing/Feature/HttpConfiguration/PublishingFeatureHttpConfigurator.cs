using EdFi.Ods.Api.Architecture;
using EdFi.Ods.Common;
using EdFi.Ods.Extensions.Publishing.Feature.SnapshotContext;

namespace EdFi.Ods.Extensions.Publishing.Feature.HttpConfiguration
{
    public class PublishingFeatureHttpConfigurator : IHttpConfigurator
    {
        private readonly ISnapshotContextProvider _snapshotContextProvider;

        public PublishingFeatureHttpConfigurator(ISnapshotContextProvider snapshotContextProvider)
        {
            _snapshotContextProvider = Preconditions.ThrowIfNull(snapshotContextProvider, nameof(snapshotContextProvider));
        }

        public void Configure(System.Web.Http.HttpConfiguration config)
        {
            Preconditions.ThrowIfNull(config, nameof(config));

            config.Filters.Add(new SnapshotContextActionFilter(_snapshotContextProvider));
        }
    }
}