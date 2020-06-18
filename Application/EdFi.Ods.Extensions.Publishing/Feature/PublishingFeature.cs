using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using EdFi.Ods.Common;
using EdFi.Ods.Common.Configuration;
using EdFi.Ods.Common.Extensibility;
using EdFi.Ods.Extensions.Publishing._Installers;

namespace EdFi.Ods.Extensions.Publishing.Feature
{
    public class PublishingFeature : ConfigurationBasedFeatureBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PublishingFeature"/>.
        /// </summary>
        /// <param name="configValueProvider">
        /// An instance of <see cref="IConfigValueProvider"/>, which is used to determine if the feature is enabled.
        /// </param>
        /// <param name="apiConfigurationProvider">An instance of a service providing API configuration details.</param>
        public PublishingFeature(IConfigValueProvider configValueProvider, IApiConfigurationProvider apiConfigurationProvider)
            : base(configValueProvider, apiConfigurationProvider) { }

        public override string FeatureName => PublishingConstants.FeatureName;

        public override IWindsorInstaller Installer => new PublishingInstaller();
    }
}
