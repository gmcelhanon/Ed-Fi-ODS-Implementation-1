using System;
using System.Data.SqlClient;
using System.Net;
using EdFi.Ods.Api.ExceptionHandling;
using EdFi.Ods.Common.Extensions;
using EdFi.Ods.Extensions.Publishing.Feature.SnapshotContext;
using NHibernate.Exceptions;

namespace EdFi.Ods.Extensions.Publishing.Feature.ExceptionHandling.SqlServer 
{
    public class SqlServerSnapshotReadOnlyDatabaseExceptionTranslator : IExceptionTranslator
    {
        private readonly ISnapshotContextProvider _snapshotContextProvider;

        public SqlServerSnapshotReadOnlyDatabaseExceptionTranslator(ISnapshotContextProvider snapshotContextProvider)
        {
            _snapshotContextProvider = snapshotContextProvider;
        }
        
        public bool TryTranslateMessage(Exception ex, out RESTError webServiceError)
        {
            webServiceError = null;

            var exception = ex is GenericADOException
                ? ex.InnerException
                : ex;

            if (exception is SqlException)
            {
                if (exception.Message.EndsWith("because the database is read-only.")
                    && _snapshotContextProvider.GetSnapshotContext() != null)
                {
                    webServiceError = new RESTError
                    {
                        Code = (int) HttpStatusCode.MethodNotAllowed,
                        Type = HttpStatusCode.MethodNotAllowed.ToString().NormalizeCompositeTermForDisplay(), 
                        Message = "Snapshots are read-only."
                    };

                    return true;
                }
            }

            return false;
        }
    }
}