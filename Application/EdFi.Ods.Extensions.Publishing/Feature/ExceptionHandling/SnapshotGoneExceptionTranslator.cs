using System;
using System.Net;
using EdFi.Ods.Api.ExceptionHandling;
using EdFi.Ods.Api.Exceptions;
using EdFi.Ods.Common;
using EdFi.Ods.Common.Extensions;
using EdFi.Ods.Extensions.Publishing.Feature.SnapshotContext;
using NHibernate.Exceptions;

namespace EdFi.Ods.Extensions.Publishing.Feature.ExceptionHandling
{
    public class SnapshotGoneExceptionTranslator : IExceptionTranslator
    {
        private readonly ISnapshotContextProvider _snapshotContextProvider;

        public SnapshotGoneExceptionTranslator(ISnapshotContextProvider snapshotContextProvider)
        {
            _snapshotContextProvider = snapshotContextProvider;
        }

        public bool TryTranslateMessage(Exception ex, out RESTError webServiceError)
        {
            Preconditions.ThrowIfNull(ex, nameof(ex));
            
            webServiceError = null;

            // Unwrap the NHibernate generic exception if it is present
            var exception = ex is GenericADOException
                ? ex.InnerException
                : ex;
            
            if (!(exception is DatabaseConnectionException)
                || _snapshotContextProvider.GetSnapshotContext() == null)
            {
                return false;
            }

            webServiceError = new RESTError
            {
                Code = (int) HttpStatusCode.Gone,
                Type = HttpStatusCode.Gone.ToString().NormalizeCompositeTermForDisplay(), 
                Message = "Snapshot not available."
            };

            return true;
        }
    }
}
