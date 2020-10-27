using System;
using System.Data.SqlClient;
using System.Linq;
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

        private readonly int[] _sqlServerErrorNumbers =
        {
            // Cannot open database "{database_name}" requested by the login. The login failed.
            // https://docs.microsoft.com/en-us/sql/relational-databases/errors-events/database-engine-events-and-errors?view=sql-server-ver15
            4060,

            // A connection was successfully established with the server, but then an error occurred during the login process.
            // https://docs.microsoft.com/en-us/sql/relational-databases/errors-events/mssqlserver-233-database-engine-error?view=sql-server-ver15
            233
        };

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

            if (exception is SqlException sqlException)
            {
                if (_sqlServerErrorNumbers.Contains(sqlException.Number)
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