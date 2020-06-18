using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using EdFi.Ods.Api.Models.Requests.Publishing.Snapshots;
using EdFi.Ods.Api.Services.CustomActionResults;

namespace EdFi.Ods.Api.Services.Controllers.Publishing.Snapshots
{
    public partial class SnapshotsController
    {
        public override async Task<IHttpActionResult> Post(SnapshotPost request) => await Task.FromResult(MethodNotAllowed());

        public override async Task<IHttpActionResult> Put(SnapshotPut request, Guid id) => await Task.FromResult(MethodNotAllowed());

        public override async Task<IHttpActionResult> Delete(Guid id) => await Task.FromResult(MethodNotAllowed());

        private IHttpActionResult MethodNotAllowed()
        {
            return new StatusCodeResult(HttpStatusCode.MethodNotAllowed, this)
                .With(
                    resp =>
                    {
                        resp.Content = new StringContent("");
                        resp.Content.Headers.Add("Allow", "GET");
                    });
        }
    }
}
