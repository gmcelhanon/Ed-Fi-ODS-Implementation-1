using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using EdFi.Ods.Common;
using EdFi.Ods.Common.Configuration;
using EdFi.Ods.Extensions.Publishing._Installers;

namespace EdFi.Ods.Extensions.Publishing.Feature
{
    public class Publishing
    {
        public const string FeatureName = "Publishing";
        public const string FeatureVersion = "1";

        /// <summary>
        /// Metadata Route name for Change Queries
        /// </summary>
        // public static readonly string ChangeQueriesMetadataRouteName = EdFiConventions.GetOpenApiMetadataRouteName(FeatureName);
    }

    public class PublishingFeature : ConfigurationBasedFeature
    {
        protected override string FeatureName => Publishing.FeatureName;

        /// <summary>
        /// Initializes a new instance of <see cref="PublishingFeature"/>.
        /// </summary>
        /// <param name="configValueProvider">
        /// An instance of <see cref="IConfigValueProvider"/>, which is used to determine if the feature is enabled.
        /// </param>
        public PublishingFeature(IConfigValueProvider configValueProvider)
            : base(configValueProvider) { }

        /// <summary>
        /// List of <see cref="WindsorInstaller"/> objects for this feature.
        /// </summary>
        /// <returns>
        /// List of <see cref="WindsorInstaller"/> objects.
        /// </returns>
        public override IList<IWindsorInstaller> Installers() => new List<IWindsorInstaller>
        {
            new PublishingInstaller()
        };
    }
}
