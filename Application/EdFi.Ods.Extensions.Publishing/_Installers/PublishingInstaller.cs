using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EdFi.Ods.Api.Architecture;
using EdFi.Ods.Api.ExceptionHandling;
using EdFi.Ods.Common.Database;
using EdFi.Ods.Common.InversionOfControl;
using EdFi.Ods.Extensions.Publishing.Feature.DatabaseNaming;
using EdFi.Ods.Extensions.Publishing.Feature.ExceptionHandling;
using EdFi.Ods.Extensions.Publishing.Feature.ExceptionHandling.SqlServer;
using EdFi.Ods.Extensions.Publishing.Feature.HttpConfiguration;
using EdFi.Ods.Extensions.Publishing.Feature.SnapshotContext;

namespace EdFi.Ods.Extensions.Publishing._Installers
{
    public class PublishingInstaller : RegistrationMethodsInstallerBase
    {
        protected virtual void RegisterIPublishingFeatureHttpConfigurator(IWindsorContainer container)
        {
            container.Register(
                Component
                    .For<IHttpConfigurator>()
                    .ImplementedBy<PublishingFeatureHttpConfigurator>());
        }

        protected virtual void RegisterISnapshotContextProvider(IWindsorContainer container)
        {
            container.Register(
                Component
                    .For<ISnapshotContextProvider>()
                    .ImplementedBy<SnapshotContextProvider>());
        }

        protected virtual void RegisterIDatabaseNameProviderDecorator(IWindsorContainer container)
        {
            container.Register(
                Component
                    .For<IDatabaseNameProvider>()
                    .ImplementedBy<SnapshotSuffixDatabaseNameProviderDecorator>());
        }

        protected virtual void RegisterExceptionTranslators(IWindsorContainer container)
        {
            container.Register(
                Component
                    .For<IExceptionTranslator>()
                    .ImplementedBy<SnapshotGoneExceptionTranslator>());

            // This one handles SQL-specific exceptions to achieve 410 Gone response
            container.Register(
                Component
                    .For<IExceptionTranslator>()
                    .ImplementedBy<SqlServerSnapshotConnectionExceptionTranslator>());

            // This one handles SQL-specific exceptions to achieve 405 Method Not Allowed response
            // when a snapshot context is in place and the SQL Database is read-only.
            container.Register(
                Component
                    .For<IExceptionTranslator>()
                    .ImplementedBy<SqlServerSnapshotReadOnlyDatabaseExceptionTranslator>());
        }
    }
}
