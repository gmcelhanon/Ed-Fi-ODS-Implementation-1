using System;
using System.Data.SqlClient;
using System.Net;
using EdFi.Common.Extensions;
using EdFi.Ods.Api.ExceptionHandling;
using EdFi.Ods.Api.Models;
using EdFi.Ods.Common.Extensions;
using EdFi.Ods.Extensions.Publishing.Feature.SnapshotContext;
using NHibernate.Exceptions;

namespace EdFi.Ods.Extensions.Publishing.Feature.ExceptionHandling.SqlServer
{
    public class SqlServerSnapshotConnectionExceptionTranslator : IExceptionTranslator
    {
        private readonly ISnapshotContextProvider _snapshotContextProvider;

        public SqlServerSnapshotConnectionExceptionTranslator(ISnapshotContextProvider snapshotContextProvider)
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
                if ((exception.Message.StartsWith("Cannot open database")
                        || exception.Message.StartsWith("A connection was successfully established with the server, but then an error occurred during the login process."))
                    && _snapshotContextProvider.GetSnapshotContext() != null)
                {
                    webServiceError = new RESTError
                    {
                        Code = (int) HttpStatusCode.Gone,
                        Type = HttpStatusCode.Gone.ToString().NormalizeCompositeTermForDisplay(), 
                        Message = "Snapshot not available."
                    };

                    return true;
                }
            }

            return false;
        }
    }
}