using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EdFi.Common.Extensions;
using EdFi.Ods.Api.Services.Controllers.Publishing.Snapshots;
using EdFi.Ods.Common.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace EdFi.Ods.Extensions.Publishing.Feature.SnapshotContext
{
    /// <summary>
    /// Implements an action filter that sets the <see cref="SnapshotContext" /> based on the
    /// "Snapshot-Identifier" header value supplied on the current API request, if applicable.
    /// </summary>
    public class SnapshotContextActionFilter : IAsyncActionFilter
    {
        public const string SnapshotIdentifierHeaderName = "Snapshot-Identifier";
        
        private readonly ISnapshotContextProvider _snapshotContextProvider;

        public SnapshotContextActionFilter(ISnapshotContextProvider snapshotContextProvider)
        {
            _snapshotContextProvider = snapshotContextProvider;
        }
        
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Don't process "snapshot" requests against a snapshot database
            if (context.Controller is SnapshotsController)
            {
                return next();
            }

            if (context.HttpContext.Request.Headers.TryGetValue(SnapshotIdentifierHeaderName, out StringValues values))
            {
                // Ensure attempts to modify the snapshot are prevented
                if (!context.HttpContext.Request.Method.EqualsIgnoreCase(HttpMethod.Get.ToString())
                    && !context.HttpContext.Request.Method.EqualsIgnoreCase(HttpMethod.Options.ToString()))
                {
                    throw new MethodNotAllowedException("Snapshots are read-only.");
                }
                
                _snapshotContextProvider.SetSnapshotContext(
                    new SnapshotContext(values.FirstOrDefault()));
            }

            return next();
        }
    }
}
