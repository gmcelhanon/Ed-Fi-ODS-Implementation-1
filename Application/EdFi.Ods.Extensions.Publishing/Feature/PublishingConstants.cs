using EdFi.Ods.Common.Conventions;

namespace EdFi.Ods.Extensions.Publishing.Feature
{
    public static class PublishingConstants
    {
        public const string FeatureName = "Publishing";
        public const string FeatureVersion = "1";
        
        /// <summary>
        /// Metadata Route name for Change Queries
        /// </summary>
        public static readonly string ChangeQueriesMetadataRouteName = EdFiConventions.GetOpenApiMetadataRouteName(FeatureName);
    }
}
