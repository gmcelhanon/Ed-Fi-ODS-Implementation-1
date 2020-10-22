using System;
using System.Net;
using System.Threading.Tasks;
using EdFi.Ods.Api.Common.Models.Requests.Publishing.Snapshots;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EdFi.Ods.Api.Services.Controllers.Publishing.Snapshots
{
    public partial class SnapshotsController
    {
        public override async Task<IActionResult> Post(SnapshotPost request) => await Task.FromResult(new MethodNotAllowedResult());

        public override async Task<IActionResult> Put(SnapshotPut request, Guid id) => await Task.FromResult(new MethodNotAllowedResult());

        public override async Task<IActionResult> Delete(Guid id) => await Task.FromResult(new MethodNotAllowedResult());
    
        public class MethodNotAllowedResult : ActionResult, IClientErrorActionResult, IStatusCodeActionResult, IActionResult
        {
            int? IStatusCodeActionResult.StatusCode => (int) HttpStatusCode.MethodNotAllowed;

            public override void ExecuteResult(ActionContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                // context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger<StatusCodeResult>().HttpStatusCodeResultExecuting(this.StatusCode);
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.MethodNotAllowed;
                context.HttpContext.Response.Headers.Add("Allow", "GET");
            }
        }

    }
}
