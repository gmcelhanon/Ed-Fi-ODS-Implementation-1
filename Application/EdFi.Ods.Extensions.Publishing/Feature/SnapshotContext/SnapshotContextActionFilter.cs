using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using EdFi.Ods.Api.Services.Controllers.Publishing.Snapshots;
using EdFi.Ods.Common;
using EdFi.Ods.Common.Exceptions;

namespace EdFi.Ods.Extensions.Publishing.Feature.SnapshotContext
{
    public class SnapshotContextActionFilter : IActionFilter
    {
        public const string SnapshotIdentifierHeaderName = "Snapshot-Identifier";
        
        private readonly ISnapshotContextProvider _snapshotContextProvider;

        public SnapshotContextActionFilter(ISnapshotContextProvider snapshotContextProvider)
        {
            _snapshotContextProvider = Preconditions.ThrowIfNull(snapshotContextProvider, nameof(snapshotContextProvider));
        }
        
        public bool AllowMultiple { get; } = false;

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(
            HttpActionContext actionContext, 
            CancellationToken cancellationToken, 
            Func<Task<HttpResponseMessage>> continuation)
        {
            // Don't process "snapshots" requests against a snapshot database
            if (actionContext.ControllerContext.Controller is SnapshotsController)
                return continuation();
            
            IEnumerable<string> values;

            if (actionContext.Request.Headers.TryGetValues(SnapshotIdentifierHeaderName, out values))
            {
                if (actionContext.Request.Method != HttpMethod.Get
                    && actionContext.Request.Method != HttpMethod.Options)
                {
                    throw new MethodNotAllowedException("Snapshots are read-only.");
                }
                
                _snapshotContextProvider.SetSnapshotContext(
                    new SnapshotContext(values.FirstOrDefault()));
            }

            return continuation();
        }
    }
}
