using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using EdFi.Common.Extensions;
using EdFi.Ods.Api.ExceptionHandling;
using EdFi.Ods.Api.Models;
using EdFi.Ods.Extensions.Publishing.Feature.SnapshotContext;
using NHibernate.Exceptions;

namespace EdFi.Ods.Extensions.Publishing.Feature.ExceptionHandling.SqlServer 
{
    public class SqlServerSnapshotReadOnlyDatabaseExceptionTranslator : IExceptionTranslator
    {
        private readonly ISnapshotContextProvider _snapshotContextProvider;

        private readonly int[] _sqlServerErrorNumbers =
        {
            // Failed to update database "{database_name}" because the database is read-only.
            3906,
        };
        
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

            if (exception is SqlException sqlException)
            {
                if (_sqlServerErrorNumbers.Contains(sqlException.Number)
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