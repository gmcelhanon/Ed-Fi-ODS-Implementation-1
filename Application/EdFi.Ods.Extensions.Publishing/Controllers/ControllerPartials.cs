using System;
using System.Threading.Tasks;
using EdFi.Ods.Api.Common.Models.Requests.Publishing.Snapshots;
using EdFi.Ods.Extensions.Publishing.Feature;
using EdFi.Ods.Extensions.Publishing.Feature.ActionResults;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace EdFi.Ods.Api.Services.Controllers.Publishing.Snapshots
{
    public partial class SnapshotsController
    {
        // NOTE: The SnapshotContextActionFilter should prevent these methods from executing,
        // but they are overridden here to ensure that no data modification can happen.
        public override async Task<IActionResult> Post(SnapshotPost request) 
            => await Task.FromResult(new SnapshotsAreReadOnlyResult());

        public override async Task<IActionResult> Put(SnapshotPut request, Guid id) 
            => await Task.FromResult(new SnapshotsAreReadOnlyResult());

        public override async Task<IActionResult> Delete(Guid id) 
            => await Task.FromResult(new SnapshotsAreReadOnlyResult());
    }
}
