using Autofac;
using EdFi.Ods.Api.ExceptionHandling;
using EdFi.Ods.Common.Configuration;
using EdFi.Ods.Common.Constants;
using EdFi.Ods.Common.Container;
using EdFi.Ods.Common.Database;
using EdFi.Ods.Extensions.Publishing.Feature.DatabaseNaming;
using EdFi.Ods.Extensions.Publishing.Feature.ExceptionHandling;
using EdFi.Ods.Extensions.Publishing.Feature.ExceptionHandling.SqlServer;
using EdFi.Ods.Extensions.Publishing.Feature.SnapshotContext;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EdFi.Ods.Extensions.Publishing.Feature
{
    public class PublishingModule : ConditionalModule
    {
        public PublishingModule(ApiSettings apiSettings)
            : base(apiSettings, nameof(PublishingModule)) { }

        public override bool IsSelected() => IsFeatureEnabled(ApiFeature.Publishing);

        public override void ApplyConfigurationSpecificRegistrations(ContainerBuilder builder)
        {
            builder.RegisterType<SnapshotContextProvider>()
                    .As<ISnapshotContextProvider>();

            builder.RegisterType<SnapshotContextActionFilter>()
                .As<IFilterMetadata>()
                .SingleInstance();
            
            builder.RegisterDecorator<SnapshotSuffixDatabaseNameReplacementTokenProvider, IDatabaseNameReplacementTokenProvider>();

            builder.RegisterType<SnapshotGoneExceptionTranslator>()
                .As<IExceptionTranslator>();

            // This one handles SQL-specific exceptions to achieve 410 Gone response
            builder.RegisterType<SqlServerSnapshotConnectionExceptionTranslator>()
                .As<IExceptionTranslator>();

            // This one handles SQL-specific exceptions to achieve 405 Method Not Allowed response
            // when a snapshot context is provided and the API client attempts to modify
            // data against the read-only SQL Database.
            builder.RegisterType<SqlServerSnapshotReadOnlyDatabaseExceptionTranslator>()
                .As<IExceptionTranslator>();
        }
    }
}
